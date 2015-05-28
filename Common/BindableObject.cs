using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Common
{
    /// <summary>
    /// Implements the INotifyPropertyChanged interface and 
    /// exposes a RaisePropertyChanged method for derived 
    /// classes to raise the PropertyChange event.  The event 
    /// arguments created by this class are cached to prevent 
    /// managed heap fragmentation.
    /// </summary>
    [Serializable]
    public abstract class BindableObject : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        #region Constructors

        static BindableObject()
        {
            eventArgCache = new Dictionary<string, PropertyChangedEventArgs>();
        }

        protected BindableObject()
        {
            _errors = new Dictionary<string, List<ValidationResult>>();
        }

        #endregion // Constructors

        #region INotifyPropertyChanged

        #region Data

        private static readonly Dictionary<string, PropertyChangedEventArgs> eventArgCache;
        private const string ERROR_MSG = "{0} is not a public property of {1}";

        #endregion // Data

        #region Public Members

        /// <summary>
        /// Raised when a public property of this object is set.
        /// </summary>
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Returns an instance of PropertyChangedEventArgs for 
        /// the specified property name.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property to create event args for.
        /// </param>
        public static PropertyChangedEventArgs
            GetPropertyChangedEventArgs(string propertyName)
        {
            if (String.IsNullOrEmpty(propertyName))
                throw new ArgumentException(
                    "propertyName cannot be null or empty.");

            PropertyChangedEventArgs args;

            // Get the event args from the cache, creating them
            // and adding to the cache if necessary.
            lock (typeof(BindableObject))
            {
                bool isCached = eventArgCache.ContainsKey(propertyName);
                if (!isCached)
                {
                    eventArgCache.Add(
                        propertyName,
                        new PropertyChangedEventArgs(propertyName));
                }

                args = eventArgCache[propertyName];
            }

            return args;
        }

        #endregion // Public Members

        #region Protected Members

        /// <summary>
        /// Derived classes can override this method to
        /// execute logic after a property is set. The 
        /// base implementation does nothing.
        /// </summary>
        /// <param name="propertyName">
        /// The property which was changed.
        /// </param>
        protected virtual void AfterPropertyChanged(string propertyName)
        {
        }

        /// <summary>
        /// Attempts to raise the PropertyChanged event, and 
        /// invokes the virtual AfterPropertyChanged method, 
        /// regardless of whether the event was raised or not.
        /// </summary>
        /// <param name="propertyName">
        /// The property which was changed.
        /// </param>
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                // Get the cached event args.
                PropertyChangedEventArgs args =
                    GetPropertyChangedEventArgs(propertyName);

                // Raise the PropertyChanged event.
                handler(this, args);
            }

            this.AfterPropertyChanged(propertyName);
        }

        #endregion // Protected Members

        #endregion

        #region INotifyDataErrorInfo

        protected readonly Dictionary<string, Func<List<ValidationResult>>> _validators = new Dictionary<string, Func<List<ValidationResult>>>();

        private static readonly ValidationResult[] noErrors = new ValidationResult[0];

        [NonSerialized]
        private readonly Dictionary<string, List<ValidationResult>> _errors;
        [NonSerialized]
        private bool hasErrors;

        /// <summary>
        /// Gets a value that indicates whether the entity has validation errors.
        /// </summary>
        public bool HasErrors
        {
            get { return hasErrors; }
            private set { SetProperty(ref hasErrors, value); }
        }

        /// <summary>
        /// Occurs when the validation errors have changed for a property or for the entire entity.
        /// </summary>
        [field: NonSerialized]
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// Gets the validation errors for the entire entity.
        /// </summary>
        /// <returns>The validation errors for the entity.</returns>
        public IEnumerable<ValidationResult> GetErrors()
        {
            return GetErrors(null);
        }

        /// <summary>
        /// Gets the validation errors for a specified property or for the entire entity.
        /// </summary>
        /// <param name="propertyName">The name of the property to retrieve validation errors for; 
        /// or null or String.Empty, to retrieve entity-level errors.</param>
        /// <returns>The validation errors for the property or entity.</returns>
        public IEnumerable<ValidationResult> GetErrors(string propertyName)
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                List<ValidationResult> result;
                if (_errors.TryGetValue(propertyName, out result))
                {
                    return result;
                }
                return noErrors;
            }
            else
            {
                return _errors.Values.SelectMany(x => x).Distinct().ToArray();
            }
        }

        IEnumerable INotifyDataErrorInfo.GetErrors(string propertyName)
        {
            return GetErrors(propertyName);
        }

        /// <summary>
        /// Raises the <see cref="E:ErrorsChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.ComponentModel.DataErrorsChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnErrorsChanged(DataErrorsChangedEventArgs e)
        {
            EventHandler<DataErrorsChangedEventArgs> handler = ErrorsChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void RaiseErrorsChanged(string propertyName = "")
        {
            HasErrors = _errors.Any();
            OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
        }

        #endregion

        #region virtual method

        /// <summary>
        /// this method is called at the end of construtor, override to add initialize task if needed
        /// </summary>
        protected virtual void Init()
        {

        }

        /// <summary>
        /// this method is used for check if item is new added (not save to db yet)
        /// </summary>
        /// <returns>true if new, otherwise is false</returns>
        public virtual bool IsNewItem()
        {
            return ((int)this.GetType().GetProperty("Ma").GetValue(this)) == 0;
        }

        /// <summary>
        /// this method is used for compare 2 item
        /// </summary>
        /// <param name="b"></param>
        /// <returns>true if equal, otherwise is false</returns>
        public virtual bool IsEqual(BindableObject b)
        {
            var type = this.GetType();
            var m = (int)type.GetProperty("Ma").GetValue(this);
            var m1 = (int)type.GetProperty("Ma").GetValue(b);
            return m == m1;
        }

        #endregion

        #region helpers

        /// <summary>
        /// Set the property with the specified value. If the value is not equal with the field then the field is
        /// set, a PropertyChanged event is raised and it returns true.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="field">Reference to the backing field of the property.</param>
        /// <param name="value">The new value for the property.</param>
        /// <param name="propertyName">The property name. This optional parameter can be skipped
        /// because the compiler is able to create it automatically.</param>
        /// <returns>True if the value has changed, false if the old and new value were equal.</returns>
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(field, value)) { return false; }

            field = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Set the property with the specified value and validate the property. If the value is not equal with the field then the field is
        /// set, a PropertyChanged event is raised, the property is validated and it returns true.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="field">Reference to the backing field of the property.</param>
        /// <param name="value">The new value for the property.</param>
        /// <param name="propertyName">The property name. This optional parameter can be skipped
        /// because the compiler is able to create it automatically.</param>
        /// <returns>True if the value has changed, false if the old and new value were equal.</returns>
        /// <exception cref="ArgumentException">The argument propertyName must not be null or empty.</exception>
        protected bool SetPropertyAndValidate<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName)) { throw new ArgumentException("The argument propertyName must not be null or empty."); }

            if (SetProperty(ref field, value, propertyName))
            {
                ValidateProperty(value, propertyName);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Validates the property with the specified value. The validation results are stored and can be retrieved by the 
        /// GetErrors method. If the validation results are changing then the ErrorsChanged event will be raised.
        /// </summary>
        /// <param name="value">The value of the property.</param>
        /// <param name="propertyName">The property name. This optional parameter can be skipped
        /// because the compiler is able to create it automatically.</param>
        /// <returns>True if the property value is valid, otherwise false.</returns>
        /// <exception cref="ArgumentException">The argument propertyName must not be null or empty.</exception>
        protected bool ValidateProperty(object value, [CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName)) { throw new ArgumentException("The argument propertyName must not be null or empty."); }

            List<ValidationResult> validationResults = new List<ValidationResult>();
            //Validator.TryValidateProperty(value, new ValidationContext(this) { MemberName = propertyName }, validationResults);
            if (_validators.ContainsKey(propertyName))
            {
                validationResults.AddRange(_validators[propertyName]());
            }
            if (validationResults.Any())
            {
                _errors[propertyName] = validationResults;
                RaiseErrorsChanged(propertyName);
                return false;
            }
            else
            {
                if (_errors.Remove(propertyName))
                {
                    RaiseErrorsChanged(propertyName);
                }
            }
            return true;
        }

        /// <summary>
        /// Validates the object and all its properties. The validation results are stored and can be retrieved by the 
        /// GetErrors method. If the validation results are changing then the ErrorsChanged event will be raised.
        /// </summary>
        /// <returns>True if the object is valid, otherwise false.</returns>
        public bool Validate()
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();
            //Validator.TryValidateObject(this, new ValidationContext(this), validationResults, true);
            foreach (var validator in _validators)
            {
                validationResults.AddRange(validator.Value());
            }
            if (validationResults.Any())
            {
                _errors.Clear();
                foreach (var validationResult in validationResults)
                {
                    var propertyNames = validationResult.MemberNames.Any() ? validationResult.MemberNames : new string[] { "" };
                    foreach (string propertyName in propertyNames)
                    {
                        if (!_errors.ContainsKey(propertyName))
                        {
                            _errors.Add(propertyName, new List<ValidationResult>() { validationResult });
                        }
                        else
                        {
                            _errors[propertyName].Add(validationResult);
                        }
                    }
                }
                RaiseErrorsChanged();
                return false;
            }
            else
            {
                if (_errors.Any())
                {
                    _errors.Clear();
                    RaiseErrorsChanged();
                }
            }
            return true;
        }

        #endregion
    }
}
