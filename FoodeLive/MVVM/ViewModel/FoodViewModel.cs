using FoodeLive.MVVM.Model;
using FoodeLive.utils;
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

        private string _maBanAn;
        public string MaBanAn { get => _maBanAn; set { _maBanAn = value; OnPropertyChanged(); } }

        private int _soHoaDon;
        public int SoHoaDon { get => _soHoaDon; set { _soHoaDon = value; OnPropertyChanged(); } }

        private ObservableCollection<MonAn> _ListMonAn;
        public ObservableCollection<MonAn> ListMonAn { get => _ListMonAn; set { _ListMonAn = value; OnPropertyChanged(); } }


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
                foreach (ChiTietHoaDon item in DataProvider.Ins.DB.ChiTietHoaDons.Where(cthd => cthd.SoHoaDon == _soHoaDon))
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
                    long temp = (long)DataProvider.Ins.DB.HoaDons.ToList().Find(hd => hd.SoHoaDon == _soHoaDon && hd.MaBanAn == _maBanAn).TriGia;
                    _tongTien = temp;
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
                    foreach (ChiTietHoaDon item in DataProvider.Ins.DB.ChiTietHoaDons.Where(hd => hd.SoHoaDon == _soHoaDon))
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

        public ICommand AnnounceAddFood { get; set; }
        public ICommand PayCommand { get; set; }
        public ICommand AddFoodCommand { get; set; }

        public FoodViewModel()
        {
            _ListMonAn = new ObservableCollection<MonAn>(DataProvider.Ins.DB.MonAns);
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
                    int lastSoHoaDon = DataProvider.Ins.DB.HoaDons.Count();
                    HoaDon hoaDon;
                    long tempTongTien = _tongTien;
                    long tempSoMon = _soMon;
                    List<ChiTietHoaDon> chiTietHoaDons = new List<ChiTietHoaDon>();
                    // kiem tra ban trong
                    if (DataProvider.Ins.DB.BanAns.ToList().Find(b => _maBanAn == b.MaBanAn).TrangThai != "Trống")
                    {
                        if (DataProvider.Ins.DB.ChiTietDatBans.ToList().FindLast(t => t.MaBan == _maBanAn).TrangThai == 2)
                            _soHoaDon = DataProvider.Ins.DB.HoaDons.ToList().FindLast(b => _maBanAn == b.MaBanAn).SoHoaDon;
                        // Them mon an vao hoa don hien co
                        foreach (MonAn monAn in selectedCollection)
                        {
                            // thay doi so luong
                            HoaDon currentHoaDon = new HoaDon();
                            currentHoaDon = DataProvider.Ins.DB.HoaDons.ToList().FindLast(hd => hd.SoHoaDon == _soHoaDon);
                            tempSoMon++;
                            if (DataProvider.Ins.DB.ChiTietHoaDons.ToList().Exists(t => t.MaMonAn == monAn.MaMonAn && t.SoHoaDon == _soHoaDon))
                            {
                                DataProvider.Ins.DB.ChiTietHoaDons.ToList().Find(t => t.MaMonAn == monAn.MaMonAn && t.SoHoaDon == _soHoaDon).SoLuong += 1;
                                DataProvider.Ins.DB.HoaDons.ToList().Find(hd => hd.SoHoaDon == _soHoaDon && hd.MaBanAn == _maBanAn).TriGia += monAn.Gia;
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
                        hoaDon = new HoaDon() { MaBanAn = _maBanAn, MaNV = "NV01", NgayLapHoaDon = DateTime.Now, SoHoaDon = _soHoaDon, TriGia = 0 };
                        DataProvider.Ins.DB.HoaDons.Add(hoaDon);

                        foreach (MonAn monAn in selectedCollection)
                        {
                            hoaDon.TriGia += monAn.Gia;
                            tempTongTien += (int)monAn.Gia;
                            tempSoMon++;
                            ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon() { MonAn = monAn, SoHoaDon = _soHoaDon, SoLuong = 1, MaMonAn = monAn.MaMonAn, HoaDon = hoaDon };
                            chiTietHoaDons.Add(chiTietHoaDon);
                        }
                        DataProvider.Ins.DB.BanAns.ToList().FindLast(b => b.MaBanAn == _maBanAn).TrangThai = "Có khách";
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
                    DataProvider.Ins.DB.BanAns.ToList().Find(b => b.MaBanAn == _maBanAn).TrangThai = "Trống";
                    DataProvider.Ins.DB.SaveChanges();
                    _tongTien = 0;
                    _soMon = 0;
                    _soHoaDon = 0;
                    OnPropertyChanged("SoMon");
                    OnPropertyChanged("TongTien");
                    _selectedItems.Clear();
                    _selectedItems = new ObservableCollection<MoneyWithQuantities>();
                    MessageBox.Show("Đã thanh toán!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });

            AddFoodCommand = new RelayCommand<Window>(p =>
            {
                bool isNumber;
                try
                {
                    Convert.ToInt32(_themMonAn.Gia);
                    isNumber = true;
                }
                catch
                {
                    isNumber = false;
                }
                return !string.IsNullOrEmpty(_themMonAn.ImgExtension) && _themMonAn.Gia > 0 && !string.IsNullOrEmpty(_themMonAn.TenMonAn) && isNumber;
            }, p =>
            {
                try
                {
                    string fileName = _themMonAn.ImgExtension;
                    int lastMonAn = DataProvider.Ins.DB.MonAns.Count() + 1;
                    string newMaMonAn = lastMonAn < 10 ? "M0" + lastMonAn.ToString() : "M" + lastMonAn.ToString();
                    _themMonAn.MaMonAn = newMaMonAn;
                    if (fileName.Contains(".png"))

                        _themMonAn.ImgExtension = ".png";
                    else if (fileName.Contains(".jpg"))
                        _themMonAn.ImgExtension = ".jpg";
                    else
                        _themMonAn.ImgExtension = ".jpeg";

                    _themMonAn.ImgSource = newMaMonAn + _themMonAn.ImgExtension;

                    RenameFileToStandardName(fileName, newMaMonAn, _themMonAn.ImgExtension);
                    DataProvider.Ins.DB.MonAns.Add(_themMonAn);
                    DataProvider.Ins.DB.SaveChanges();
                    _ListMonAn.Add(_themMonAn);
                    MessageBox.Show("Đã thêm!");
                    p.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }
        private void RenameFileToStandardName(string fileName, string maMonAn, string exts)
        {
            if (File.Exists(fileName))
            {
                int l = fileName.LastIndexOf('\\');
                string newFileName = fileName.Substring(0, l + 1) + maMonAn + exts;
                File.Move(fileName, newFileName);
                string dir = Directory.GetCurrentDirectory();
                string root;
                if (!DevProd.IsProduction)
                    root = dir.Substring(0, dir.IndexOf("\\bin"));
                else
                    root = dir;

                string newPath = root + @"\src\static";
                string newFileToMove = Path.GetFileName(newFileName);
                MessageBox.Show(newPath + "\n" + Path.Combine(newPath, newFileToMove) + "\n" + newFileName);
                File.Copy(newFileName, Path.Combine(newPath, newFileToMove), true);
            }
        }
    }
}
