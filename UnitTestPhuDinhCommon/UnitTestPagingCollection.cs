using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestPhuDinhCommon
{
    [TestClass]
    public class UnitTestPagingCollection
    {
        [TestMethod]
        public void Test_Constructor()
        {
            var pagedCollection = new PhuDinhCommon.PagingCollection<int>(this.GetIntList());
            var result = new List<int> { pagedCollection.PageCount, pagedCollection.CurrentPage, pagedCollection.ItemsPerPage };
            CollectionAssert.AreEqual(result, new List<int> { 1, 1, 10 });
        }

        #region Test ItemPerPage
        [TestMethod]
        public void Test_ItemPerPage_ListItemCount_Is_Multiple_Of_ItemPerPage()
        {
            var pagedCollection = new PhuDinhCommon.PagingCollection<int>(this.GetIntList()) { ItemsPerPage = 2 };
            var result = new List<int> { pagedCollection.PageCount, pagedCollection.CurrentPage, pagedCollection.ItemsPerPage };
            CollectionAssert.AreEqual(result, new List<int> { 5, 1, 2 });
        }

        [TestMethod]
        public void Test_ItemPerPage_ListItemCount_Not_Multiple_Of_ItemPerPage()
        {
            var pagedCollection = new PhuDinhCommon.PagingCollection<int>(this.GetIntList()) { ItemsPerPage = 3 };
            var result = new List<int> { pagedCollection.PageCount, pagedCollection.CurrentPage, pagedCollection.ItemsPerPage };
            CollectionAssert.AreEqual(result, new List<int> { 4, 1, 3 });
        }

        [TestMethod]
        public void Test_ItemPerPage_ItemPerPage_Less_Than_one()
        {
            var pagedCollection = new PhuDinhCommon.PagingCollection<int>(this.GetIntList()) { ItemsPerPage = 0 };
            var result = new List<int> { pagedCollection.PageCount, pagedCollection.CurrentPage, pagedCollection.ItemsPerPage };
            CollectionAssert.AreEqual(result, new List<int> { 10, 1, 1 });
        }
        #endregion

        #region Test CurrentPage
        [TestMethod]
        public void Test_CurrentPage_With_CurrentPage_Less_Than_One()
        {
            var pagedCollection = new PhuDinhCommon.PagingCollection<int>(this.GetIntList()) { ItemsPerPage = 3 };

            pagedCollection.CurrentPage = 0;

            var result = new List<int> { pagedCollection.PageCount, pagedCollection.CurrentPage, pagedCollection.ItemsPerPage };
            CollectionAssert.AreEqual(result, new List<int> { 4, 1, 3 });
            CollectionAssert.AreEqual(pagedCollection.CurrentPageItems, new List<int> { 1, 2, 3 });
        }

        public void Test_CurrentPage_With_CurrentPage_Greater_Than_PageCount()
        {
            var pagedCollection = new PhuDinhCommon.PagingCollection<int>(this.GetIntList()) { ItemsPerPage = 3 };

            pagedCollection.CurrentPage = pagedCollection.PageCount + 1;

            var result = new List<int> { pagedCollection.PageCount, pagedCollection.CurrentPage, pagedCollection.ItemsPerPage };
            CollectionAssert.AreEqual(result, new List<int> { 4, 4, 3 });
            CollectionAssert.AreEqual(pagedCollection.CurrentPageItems, new List<int> { 10 });
        }

        [TestMethod]
        public void Test_CurrentPage_With_CurrentPage_Between_One_And_PageCount()
        {
            var pagedCollection = new PhuDinhCommon.PagingCollection<int>(this.GetIntList()) { ItemsPerPage = 3 };

            pagedCollection.CurrentPage = 3;

            var result = new List<int> { pagedCollection.PageCount, pagedCollection.CurrentPage, pagedCollection.ItemsPerPage };
            CollectionAssert.AreEqual(result, new List<int> { 4, 3, 3 });
            CollectionAssert.AreEqual(pagedCollection.CurrentPageItems, new List<int> { 7, 8, 9 });
        }
        #endregion

        #region Test MoveToNextPage
        [TestMethod]
        public void Test_MoveToNextPage_With_CurrentPage_Equal_PageCount()
        {
            var pagedCollection = new PhuDinhCommon.PagingCollection<int>(this.GetIntList()) { ItemsPerPage = 3 };

            pagedCollection.CurrentPage = pagedCollection.PageCount;

            var result = pagedCollection.MoveToNextPage();

            CollectionAssert.AreEqual(new List<int> { pagedCollection.PageCount, pagedCollection.CurrentPage, pagedCollection.ItemsPerPage }
                , new List<int> { 4, 4, 3 });
            CollectionAssert.AreEqual(result, new List<int> { 10 });
        }

        [TestMethod]
        public void Test_MoveToNextPage_With_CurrentPage_Between_One_And_PageCount()
        {
            var pagedCollection = new PhuDinhCommon.PagingCollection<int>(this.GetIntList()) { ItemsPerPage = 3 };

            var result = pagedCollection.MoveToNextPage();

            CollectionAssert.AreEqual(new List<int> { pagedCollection.PageCount, pagedCollection.CurrentPage, pagedCollection.ItemsPerPage }
                , new List<int> { 4, 2, 3 });
            CollectionAssert.AreEqual(result, new List<int> { 4, 5, 6 });
        }

        [TestMethod]
        public void Test_MoveToNextPage_With_CurrentPage_Less_Than_One()
        {
            var pagedCollection = new PhuDinhCommon.PagingCollection<int>(this.GetIntList()) { ItemsPerPage = 3 };

            pagedCollection.CurrentPage = 0;

            var result = pagedCollection.MoveToNextPage();

            CollectionAssert.AreEqual(new List<int> { pagedCollection.PageCount, pagedCollection.CurrentPage, pagedCollection.ItemsPerPage }
                , new List<int> { 4, 2, 3 });
            CollectionAssert.AreEqual(result, new List<int> { 4, 5, 6 });
        }
        #endregion

        #region Test MoveToPrevious
        [TestMethod]
        public void Test_MoveToPreviousPage_With_CurrentPage_Equal_PageCount()
        {
            var pagedCollection = new PhuDinhCommon.PagingCollection<int>(this.GetIntList()) { ItemsPerPage = 3 };

            pagedCollection.CurrentPage = pagedCollection.PageCount;

            var result = pagedCollection.MoveToPreviousPage();

            CollectionAssert.AreEqual(new List<int> { pagedCollection.PageCount, pagedCollection.CurrentPage, pagedCollection.ItemsPerPage }
                , new List<int> { 4, 3, 3 });
            CollectionAssert.AreEqual(result, new List<int> { 7, 8, 9 });
        }

        [TestMethod]
        public void Test_MoveToPreviousPage_With_CurrentPage_Between_One_And_PageCount()
        {
            var pagedCollection = new PhuDinhCommon.PagingCollection<int>(this.GetIntList()) { ItemsPerPage = 3 };

            pagedCollection.CurrentPage = 2;

            var result = pagedCollection.MoveToPreviousPage();

            CollectionAssert.AreEqual(new List<int> { pagedCollection.PageCount, pagedCollection.CurrentPage, pagedCollection.ItemsPerPage }
                , new List<int> { 4, 1, 3 });
            CollectionAssert.AreEqual(result, new List<int> { 1, 2, 3 });
        }

        [TestMethod]
        public void Test_MoveToPreviousPage_With_CurrentPage_Less_Than_One()
        {
            var pagedCollection = new PhuDinhCommon.PagingCollection<int>(this.GetIntList()) { ItemsPerPage = 3 };

            pagedCollection.CurrentPage = 0;

            var result = pagedCollection.MoveToPreviousPage();

            CollectionAssert.AreEqual(new List<int> { pagedCollection.PageCount, pagedCollection.CurrentPage, pagedCollection.ItemsPerPage }
                , new List<int> { 4, 1, 3 });
            CollectionAssert.AreEqual(result, new List<int> { 1, 2, 3 });
        }
        #endregion

        #region Test MoveToFirstPage
        [TestMethod]
        public void Test_MoveToFirstPage_With_CurrentPage_Less_Than_One()
        {
            var pagedCollection = new PhuDinhCommon.PagingCollection<int>(this.GetIntList()) { ItemsPerPage = 3 };

            pagedCollection.CurrentPage = 0;

            var result = pagedCollection.MoveToFirstPage();

            CollectionAssert.AreEqual(new List<int> { pagedCollection.PageCount, pagedCollection.CurrentPage, pagedCollection.ItemsPerPage }
                , new List<int> { 4, 1, 3 });
            CollectionAssert.AreEqual(result, new List<int> { 1, 2, 3 });
        }

        [TestMethod]
        public void Test_MoveToFirstPage_With_CurrentPage_Greater_Than_One()
        {
            var pagedCollection = new PhuDinhCommon.PagingCollection<int>(this.GetIntList()) { ItemsPerPage = 3 };

            pagedCollection.CurrentPage = 3;

            var result = pagedCollection.MoveToFirstPage();

            CollectionAssert.AreEqual(new List<int> { pagedCollection.PageCount, pagedCollection.CurrentPage, pagedCollection.ItemsPerPage }
                , new List<int> { 4, 1, 3 });
            CollectionAssert.AreEqual(result, new List<int> { 1, 2, 3 });
        }
        #endregion

        #region Test MoveToLastPage
        [TestMethod]
        public void Test_MoveToLastPage_With_CurrentPage_Less_Than_PageCount()
        {
            var pagedCollection = new PhuDinhCommon.PagingCollection<int>(this.GetIntList()) { ItemsPerPage = 3 };

            pagedCollection.CurrentPage = 0;

            var result = pagedCollection.MoveToLastPage();

            CollectionAssert.AreEqual(new List<int> { pagedCollection.PageCount, pagedCollection.CurrentPage, pagedCollection.ItemsPerPage }
                , new List<int> { 4, 4, 3 });
            CollectionAssert.AreEqual(result, new List<int> { 10 });
        }

        [TestMethod]
        public void Test_MoveToLastPage_With_CurrentPage_Greater_Than_PageCount()
        {
            var pagedCollection = new PhuDinhCommon.PagingCollection<int>(this.GetIntList()) { ItemsPerPage = 3 };

            pagedCollection.CurrentPage = pagedCollection.PageCount + 1;

            var result = pagedCollection.MoveToLastPage();

            CollectionAssert.AreEqual(new List<int> { pagedCollection.PageCount, pagedCollection.CurrentPage, pagedCollection.ItemsPerPage }
                , new List<int> { 4, 4, 3 });
            CollectionAssert.AreEqual(result, new List<int> { 10 });
        }
        #endregion

        private List<int> GetIntList()
        {
            return new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        }
    }
}
