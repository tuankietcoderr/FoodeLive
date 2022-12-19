using FoodeLive.Converter;
using FoodeLive.MVVM.Model;
using FoodeLive.utils;
using FoodeLive.View.Windows.CRUD.Menu;
using IT008_DoAnCuoiKi.ViewModel;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FoodeLive.MVVM.ViewModel
{

    public class FoodViewModel : BaseViewModel
    {
        private NguoiQuanLy _nguoiQuanLy;
        public NguoiQuanLy NguoiQuanLy { get => _nguoiQuanLy; set { _nguoiQuanLy = value; OnPropertyChanged(); } }

        private NhanVien _nhanVienHoatDong;
        public NhanVien NhanVienHoatDong { get => _nhanVienHoatDong; set { _nhanVienHoatDong = value; OnPropertyChanged(); } }

        private CuaHang _cuaHangHoatDong;
        public CuaHang CuaHangHoatDong { get => _cuaHangHoatDong; set { _cuaHangHoatDong = value; OnPropertyChanged(); } }

        private string _maBanAn;
        public string MaBanAn { get => _maBanAn; set { _maBanAn = value; OnPropertyChanged(); } }

        private int _soHoaDon;
        public int SoHoaDon { get => _soHoaDon; set { _soHoaDon = value; OnPropertyChanged(); } }

        private ObservableCollection<MonAn> _ListMonAn;
        public ObservableCollection<MonAn> ListMonAn
        {
            get
            {
                ObservableCollection<MonAn> monAns;
                monAns = new ObservableCollection<MonAn>(_cuaHangHoatDong.MonAns);
                return monAns;
            }
            set { _ListMonAn = value; OnPropertyChanged(); }
        }

        private ObservableCollection<string> _ListTenMonAn;
        public ObservableCollection<string> ListTenMonAn
        {
            get
            {
                ObservableCollection<string> tenMonAns = new ObservableCollection<string>();
                foreach (var item in _ListMonAn)
                    tenMonAns.Add(item.TenMonAn.ToLower());
                _ListTenMonAn = tenMonAns;
                return tenMonAns;
            }
            set
            {
                _ListTenMonAn = value;
                OnPropertyChanged();
            }
        }


        public class MoneyWithQuantities
        {
            public MonAn MonAn { get; set; }
            public int Quantity { get; set; }
            public MoneyWithQuantities()
            {
                MonAn = new MonAn();
                Quantity = 0;
            }
            public MoneyWithQuantities(MonAn monAn, int quantity)
            {
                MonAn = new MonAn();
                MonAn = monAn;
                Quantity = quantity;
            }
        }
        private ObservableCollection<MoneyWithQuantities> _selectedItems;
        public ObservableCollection<MoneyWithQuantities> SelectedItems
        {
            get
            {
                ObservableCollection<MoneyWithQuantities> temp = new ObservableCollection<MoneyWithQuantities>();
                foreach (ChiTietHoaDon item in DataProvider.Ins.DB.ChiTietHoaDons.Where(cthd => cthd.MonAn.MaCuaHang == _cuaHangHoatDong.MaCuaHang && cthd.SoHoaDon == _soHoaDon))
                {
                    MoneyWithQuantities mwq = new MoneyWithQuantities(item.MonAn, (int)item.SoLuong);
                    temp.Add(mwq);
                };
                _selectedItems = temp;
                return _selectedItems;
            }
            set
            {
                _selectedItems = value;
                OnPropertyChanged();
            }

        }

        private long _tongTien;
        public long TongTien
        {
            get
            {
                if (_soHoaDon != 0) // co khach chan chac co hoa don
                {
                    try
                    {
                        if (DataProvider.Ins.DB.HoaDons.ToList().Find(t => t.BanAn.MaCuaHang == _cuaHangHoatDong.MaCuaHang && _soHoaDon == t.SoHoaDon) != null)
                        {
                            long temp = (long)DataProvider.Ins.DB.HoaDons.ToList().Find(t => t.BanAn.MaCuaHang == _cuaHangHoatDong.MaCuaHang && _soHoaDon == t.SoHoaDon).TriGia;
                            _tongTien = temp;
                        }
                    }
                    catch
                    {
                        _tongTien = 0;
                    }
                    return _tongTien;
                }
                return 0;
            }
            set { _tongTien = value; OnPropertyChanged(); }
        }

        private long _soMon;
        public long SoMon
        {
            get
            {
                if (_soHoaDon != 0) // co khach chan chac co hoa don
                {
                    int temp = 0;
                    foreach (ChiTietHoaDon item in DataProvider.Ins.DB.ChiTietHoaDons.Where(hd => hd.SoHoaDon == _soHoaDon && hd.MonAn.MaCuaHang == _cuaHangHoatDong.MaCuaHang))
                        temp += (int)item.SoLuong;
                    _soMon = temp;
                    return _soMon;
                }
                return 0;
            }
            set { _soMon = value; OnPropertyChanged(); }
        }

        public class MonAnWithQuantity
        {
            public string MaMonAn;
            public int Quantity;
            public MonAnWithQuantity()
            {
                Quantity = 0;
                MaMonAn = string.Empty;
            }

            public MonAnWithQuantity(string maMonAn, int quantity)
            {
                MaMonAn = maMonAn;
                Quantity = quantity;
            }
        }


        private bool _ordered = false;
        public bool Ordered { get => _ordered; set { _ordered = value; OnPropertyChanged(); } }

        private MonAn _themMonAn;
        public MonAn ThemMonAn { get => _themMonAn; set { _themMonAn = value; OnPropertyChanged(); } }

        private ObservableCollection<MonAn> _searchResultsForFood;
        public ObservableCollection<MonAn> SearchResultsForFood
        {
            get
            {
                _searchResultsForFood = _ListMonAn;
                return _searchResultsForFood;
            }
            set
            {
                _searchResultsForFood = value;
                OnPropertyChanged();
            }
        }

        public ICommand AnnounceAddFood { get; set; }
        public ICommand PayCommand { get; set; }
        public ICommand AddFoodDialogCommand { get; set; }
        public ICommand AddFoodCommand { get; set; }

        public ICommand SearchFoodCommand { get; set; }
        public ICommand RefreshAllFoodCommand { get; set; }

        public FoodViewModel()
        {
            _selectedItems = new ObservableCollection<MoneyWithQuantities>();
            _tongTien = 0;
            _themMonAn = new MonAn();
            // Khi an thong bao, ma hoa don moi se duoc tao ra
            List<MonAn> selectedCollection = new List<MonAn>();

            AnnounceAddFood = new RelayCommand<object>(p =>
            {
                selectedCollection.Clear();
                System.Collections.IList items = (System.Collections.IList)p;
                selectedCollection = items.Cast<MonAn>().ToList();
                _ordered = selectedCollection.Count > 0;
                return _ordered;
            }, p =>
            {
                try
                {
                    int lastSoHoaDon = DataProvider.Ins.DB.HoaDons.ToList().Where(t => t.BanAn.MaCuaHang == _cuaHangHoatDong.MaCuaHang).Count();
                    HoaDon hoaDon;
                    long tempTongTien = _tongTien;
                    long tempSoMon = _soMon;
                    List<ChiTietHoaDon> chiTietHoaDons = new List<ChiTietHoaDon>();
                    // kiem tra ban trong
                    if (_cuaHangHoatDong.BanAns.ToList().Find(b => _maBanAn == b.MaBanAn).TrangThai != "Trống")
                    {
                        var chiTietDatBan = new ChiTietDatBan();
                        chiTietDatBan = DataProvider.Ins.DB.ChiTietDatBans.ToList().FindLast(t => t.MaBan == _maBanAn && t.BanAn.MaCuaHang == _cuaHangHoatDong.MaCuaHang);
                        if (chiTietDatBan != null && chiTietDatBan.TrangThai == 2)
                            _soHoaDon = DataProvider.Ins.DB.HoaDons.ToList().FindLast(b => _maBanAn == b.MaBanAn && b.BanAn.MaCuaHang == _cuaHangHoatDong.MaCuaHang).SoHoaDon;
                        // Them mon an vao hoa don hien co
                        foreach (MonAn monAn in selectedCollection)
                        {
                            // thay doi so luong
                            HoaDon currentHoaDon = new HoaDon();
                            currentHoaDon = DataProvider.Ins.DB.HoaDons.ToList().FindLast(hd => hd.SoHoaDon == _soHoaDon && hd.BanAn.MaCuaHang == _cuaHangHoatDong.MaCuaHang);
                            tempSoMon++;
                            if (DataProvider.Ins.DB.ChiTietHoaDons.ToList().Exists(t => t.MaMonAn == monAn.MaMonAn && t.SoHoaDon == _soHoaDon && t.MonAn.MaCuaHang == _cuaHangHoatDong.MaCuaHang))
                            {
                                DataProvider.Ins.DB.ChiTietHoaDons.ToList().Find(t => t.MaMonAn == monAn.MaMonAn && t.SoHoaDon == _soHoaDon && t.MonAn.MaCuaHang == _cuaHangHoatDong.MaCuaHang).SoLuong += 1;
                                DataProvider.Ins.DB.HoaDons.ToList().Find(hd => hd.SoHoaDon == _soHoaDon && hd.MaBanAn == _maBanAn && hd.BanAn.MaCuaHang == _cuaHangHoatDong.MaCuaHang).TriGia += monAn.Gia;
                                tempTongTien += (int)monAn.Gia;
                            }
                            else
                            {
                                currentHoaDon.TriGia += monAn.Gia;
                                tempTongTien += (int)monAn.Gia;
                                ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon() { MonAn = monAn, SoHoaDon = _soHoaDon, SoLuong = 1, MaMonAn = monAn.MaMonAn, HoaDon = currentHoaDon };
                                chiTietHoaDons.Add(chiTietHoaDon);
                            }
                        }
                    }
                    else
                    {
                        _soHoaDon = lastSoHoaDon + 1;
                        BanAn banAn = new BanAn();
                        banAn = _cuaHangHoatDong.BanAns.ToList().Find(b => b.MaBanAn == _maBanAn);
                        string maQuanLy = string.Empty;
                        if (_nguoiQuanLy != null)
                            maQuanLy = _nguoiQuanLy.MaQuanLy;
                        else
                            maQuanLy = _nhanVienHoatDong.MaQuanLy;
                        hoaDon = new HoaDon() { MaBanAn = _maBanAn, NgayLapHoaDon = DateTime.Now, SoHoaDon = _soHoaDon, TriGia = 0, BanAn = banAn, ChiTietHoaDons = chiTietHoaDons, TrangThai = 0 };
                        DataProvider.Ins.DB.HoaDons.Add(hoaDon);

                        foreach (MonAn monAn in selectedCollection)
                        {
                            hoaDon.TriGia += monAn.Gia;
                            tempTongTien += (int)monAn.Gia;
                            tempSoMon++;
                            ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon() { MonAn = monAn, SoHoaDon = _soHoaDon, SoLuong = 1, MaMonAn = monAn.MaMonAn, HoaDon = hoaDon };
                            chiTietHoaDons.Add(chiTietHoaDon);
                        }
                        DataProvider.Ins.DB.BanAns.ToList().FindLast(b => b.MaBanAn == _maBanAn && b.MaCuaHang == _cuaHangHoatDong.MaCuaHang).TrangThai = "Có khách";
                    }
                    DataProvider.Ins.DB.ChiTietHoaDons.AddRange(chiTietHoaDons);
                    DataProvider.Ins.DB.SaveChanges();
                    _tongTien = tempTongTien;
                    _soMon = tempSoMon;
                    selectedCollection.Clear();
                    MessageBox.Show("Đã thêm!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });

            PayCommand = new RelayCommand<object>(p => _soHoaDon != 0 && _tongTien > 0, p =>
            {
                try
                {
                    _ordered = true;
                    DataProvider.Ins.DB.BanAns.ToList().Find(b => b.MaBanAn == _maBanAn && b.MaCuaHang == _cuaHangHoatDong.MaCuaHang).TrangThai = "Trống";
                    var payHoaDon = DataProvider.Ins.DB.HoaDons.ToList().Find(b => b.MaBanAn == _maBanAn && b.BanAn.MaCuaHang == _cuaHangHoatDong.MaCuaHang && b.SoHoaDon == _soHoaDon);
                    payHoaDon.TrangThai = 1;
                    payHoaDon.ThoiGianThanhToan = DateTime.Now;
                    DataProvider.Ins.DB.SaveChanges();
                    _tongTien = 0;
                    _soMon = 0;
                    _soHoaDon = 0;
                    OnPropertyChanged("SoMon");
                    OnPropertyChanged("TongTien");
                    OnPropertyChanged("ListHoaDon");
                    _selectedItems.Clear();
                    _selectedItems = new ObservableCollection<MoneyWithQuantities>();
                    MessageBox.Show("Đã thanh toán!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });

            AddFoodDialogCommand = new RelayCommand<object>(p =>
            {
                return _nguoiQuanLy != null;
            }, p =>
            {
                AddToMenu addToMenu = new AddToMenu();
                addToMenu.ShowDialog();
            });

            AddFoodCommand = new RelayCommand<Window>(p =>
            {
                if (string.IsNullOrEmpty(_themMonAn.TenMonAn) || _themMonAn.Gia <= 0)
                    return false;
                return true;
            }, p =>
            {
                try
                {
                    string newMaMonAn;
                    if (DataProvider.Ins.DB.MonAns.Count() == 0)
                        _themMonAn.MaMonAn = "M01";
                    else
                    {
                        var lastMaMonAn = DataProvider.Ins.DB.MonAns.ToList().Last().MaMonAn;
                        int newIndex = Convert.ToInt32(lastMaMonAn.Substring(1, lastMaMonAn.Length - 1)) + 1;
                        newMaMonAn = newIndex < 10 ? "M0" + newIndex : "M" + newIndex;
                        _themMonAn.MaMonAn = newMaMonAn;
                    }
                    _themMonAn.MaCuaHang = _cuaHangHoatDong.MaCuaHang;
                    DataProvider.Ins.DB.MonAns.Add(_themMonAn);
                    DataProvider.Ins.DB.SaveChanges();
                    _ListMonAn.Add(_themMonAn);
                    _ListTenMonAn.Add(_themMonAn.TenMonAn);
                    _themMonAn = new MonAn();
                    OnPropertyChanged("ListMonAn");
                    OnPropertyChanged("ListTenMonAn");
                    MessageBox.Show("Đã thêm!");
                    p.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });

            SearchFoodCommand = new RelayCommand<string>(p => true, p =>
            {
                if (string.IsNullOrEmpty(p))
                    return;

                ObservableCollection<MonAn> temp = new ObservableCollection<MonAn>(_cuaHangHoatDong.MonAns.Where(m => VietnameseStringConverter.LocDau(m.TenMonAn.ToLower()).Contains(VietnameseStringConverter.LocDau(p.ToLower()))));
                _ListMonAn = temp;
                _searchResultsForFood = _ListMonAn;
                OnPropertyChanged("SearchResultsForFood");
                OnPropertyChanged("ListMonAn");
            });

            RefreshAllFoodCommand = new RelayCommand<object>(p => true, p =>
            {
                _ListMonAn = new ObservableCollection<MonAn>(_cuaHangHoatDong.MonAns);
                _searchResultsForFood = _ListMonAn;
                OnPropertyChanged("SearchResultsForFood");
                OnPropertyChanged("ListMonAn");
            });

        }
    }
}
