using System.Collections.Generic;

namespace PhuDinhData.DesignTimeData
{
    public class DDrDiaDiem
    {
        public static List<rDiaDiem> rDiaDiems = new List<rDiaDiem>()
        {
            new rDiaDiem() {Ma = 1, MaNuoc = 1, Tinh = "Tỉnh 1", Nuoc = new rNuoc(){Ma = 1, TenNuoc = "Nuoc 1"},
                rNuocList = DDrNuoc.rNuocs
            },
            new rDiaDiem() {Ma = 2, MaNuoc = 2, Tinh = "Tỉnh 2", Nuoc = new rNuoc(){Ma = 2, TenNuoc = "Nuoc 2"},
                rNuocList = DDrNuoc.rNuocs
            },
            new rDiaDiem() {Ma = 3, MaNuoc = 3, Tinh = "Tỉnh 3", Nuoc = new rNuoc(){Ma = 3, TenNuoc = "Nuoc 3"},
                rNuocList = DDrNuoc.rNuocs
            },
            new rDiaDiem() {Ma = 4, MaNuoc = 4, Tinh = "Tỉnh 4", Nuoc = new rNuoc(){Ma = 4, TenNuoc = "Nuoc 4"},
                rNuocList = DDrNuoc.rNuocs
            },
            new rDiaDiem() {Ma = 5, MaNuoc = 5, Tinh = "Tỉnh 5", Nuoc = new rNuoc(){Ma = 5, TenNuoc = "Nuoc 5"},
                rNuocList = DDrNuoc.rNuocs
            },
            new rDiaDiem() {Ma = 6, MaNuoc = 6, Tinh = "Tỉnh 6", Nuoc = new rNuoc(){Ma = 6, TenNuoc = "Nuoc 6"},
                rNuocList = DDrNuoc.rNuocs
            },
            new rDiaDiem() {Ma = 7, MaNuoc = 7, Tinh = "Tỉnh 7", Nuoc = new rNuoc(){Ma = 7, TenNuoc = "Nuoc 7"},
                rNuocList = DDrNuoc.rNuocs
            },
            new rDiaDiem() {Ma = 8, MaNuoc = 8, Tinh = "Tỉnh 8", Nuoc = new rNuoc(){Ma = 8, TenNuoc = "Nuoc 8"},
                rNuocList = DDrNuoc.rNuocs
            },
            new rDiaDiem() {Ma = 9, MaNuoc = 9, Tinh = "Tỉnh 9", Nuoc = new rNuoc(){Ma = 9, TenNuoc = "Nuoc 9"},
                rNuocList = DDrNuoc.rNuocs
            },
            new rDiaDiem() {Ma = 10, MaNuoc = 10, Tinh = "Tỉnh 10", Nuoc = new rNuoc(){Ma = 10, TenNuoc = "Nuoc 10"},
                rNuocList = DDrNuoc.rNuocs
            }
        };
    }
}
