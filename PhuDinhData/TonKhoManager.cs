using PhuDinhData.Repository;
using System;
using System.Collections.Generic;

namespace PhuDinhData
{
    public static class TonKhoManager
    {
        private static DateTime _minDate = DateTime.Now.Date;

        public static void UpdateByDonHang(List<Repository<tDonHang>.ChangedItemData> changed)
        {
            foreach (var changedItemData in changed)
            {
                if (changedItemData.CurrentValues != null && changedItemData.CurrentValues.Ngay < _minDate)
                {
                    _minDate = changedItemData.CurrentValues.Ngay;
                }
                if (changedItemData.OriginalValues != null && changedItemData.OriginalValues.Ngay < _minDate)
                {
                    _minDate = changedItemData.OriginalValues.Ngay;
                }
            }
        }

        public static void UpdateByNhapHang(List<Repository<tNhapHang>.ChangedItemData> changed)
        {
            foreach (var changedItemData in changed)
            {
                if (changedItemData.CurrentValues != null && changedItemData.CurrentValues.Ngay < _minDate)
                {
                    _minDate = changedItemData.CurrentValues.Ngay;
                }
                if (changedItemData.CurrentValues != null && changedItemData.OriginalValues.Ngay < _minDate)
                {
                    _minDate = changedItemData.OriginalValues.Ngay;
                }
            }
        }

        public static void UpdateTonKho()
        {
            BusinessLogics.BusinessLogics.UpdateTonKhosTuNgayD(ContextFactory.CreateContext(), _minDate);
        }
    }
}
