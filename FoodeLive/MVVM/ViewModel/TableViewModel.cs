﻿using FoodeLive.MVVM.Model;
using FoodeLive.Windows;
using IT008_DoAnCuoiKi.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace FoodeLive.MVVM.ViewModel
{
    public class TableViewModel : BaseViewModel
    {

        private string _maBanAn;
        public string MaBanAn { get => _maBanAn; set { _maBanAn = value; OnPropertyChanged(); } }
        private string _loai;
        public string Loai { get { return _loai; } set { _loai = value; OnPropertyChanged(); } }

        private int _soHoaDon;
        public int SoHoaDon { get => _soHoaDon; set { _soHoaDon = value; OnPropertyChanged(); } }

        private ObservableCollection<BanAn> _ListBanAn;
        public ObservableCollection<BanAn> ListBanAn { get => _ListBanAn; set { _ListBanAn = value; OnPropertyChanged(); } }

        private ObservableCollection<BanAn> _EmptyTables;
        public ObservableCollection<BanAn> EmptyTables { get => _EmptyTables; set { _EmptyTables = value; OnPropertyChanged(); } }

        private ObservableCollection<BanAn> _UsingTables;
        public ObservableCollection<BanAn> UsingTables { get => _UsingTables; set { _UsingTables = value; OnPropertyChanged(); } }

        private ObservableCollection<BanAn> _BookedTables;
        public ObservableCollection<BanAn> BookedTables { get => _BookedTables; set { _BookedTables = value; OnPropertyChanged(); } }


        private ChiTietDatBan _chiTietDatBan;
        public ChiTietDatBan ChiTietDatBan
        {
            get
            {
                if (DataProvider.Ins.DB.ChiTietDatBans.Count(c => c.MaBan == _maBanAn && c.BanAn.TrangThai == "Đã đặt") > 0)
                    _chiTietDatBan = DataProvider.Ins.DB.ChiTietDatBans.ToList().Find(c => c.MaBan == _maBanAn && c.BanAn.TrangThai == "Đã đặt");
                return _chiTietDatBan;
            }
            set { _chiTietDatBan = value; OnPropertyChanged(); }
        }

        private bool _isBooked = false;
        public bool IsBooked
        {
            get
            {
                _isBooked = DataProvider.Ins.DB.BanAns.Count(t => t.MaBanAn == _maBanAn && t.TrangThai == "Đã đặt") > 0;
                return _isBooked;
            }
            set { _isBooked = value; OnPropertyChanged(); }
        }

        public ICommand AddTableCommand { get; set; }
        public ICommand DeleteTableCommand { get; set; }
        public ICommand BookTableCommand { get; set; }
        public ICommand ApprovalBookCommand { get; set; }
        public ICommand CancelBookCommand { get; set; }

        public TableViewModel()
        {

            _chiTietDatBan = new ChiTietDatBan();
            // Add table
            AddTableCommand = new RelayCommand<Window>((p) =>
            {
                if (string.IsNullOrEmpty(_maBanAn) || _loai == "Chọn loại bàn" || _maBanAn[0] != 'B' || _maBanAn.Length != 4)
                    return false;
                var ListMaBanAn = DataProvider.Ins.DB.BanAns.Where(b => b.MaBanAn == MaBanAn).ToList();
                if (ListMaBanAn == null || ListMaBanAn.Count() != 0)
                    return false;
                return true;
            }, (p) =>
            {
                try
                {
                    var banAn = new BanAn() { MaBanAn = _maBanAn, Loai = Loai, TrangThai = "Trống" };
                    DataProvider.Ins.DB.BanAns.Add(banAn);
                    DataProvider.Ins.DB.SaveChanges();
                    _ListBanAn.Add(banAn);
                    _EmptyTables.Add(banAn);
                    MessageBox.Show("Đã thêm!");
                    p.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
            });

            // Delete table
            DeleteTableCommand = new RelayCommand<Window>((p) =>
            {
                return _maBanAn != string.Empty;
            },
            (p) =>
            {
                try
                {
                    foreach (BanAn item in DataProvider.Ins.DB.BanAns)
                    {
                        if (item.MaBanAn == _maBanAn)
                        {
                            DataProvider.Ins.DB.BanAns.Remove(item);
                            _ListBanAn.Remove(item);
                            _UsingTables.Remove(item);
                            _EmptyTables.Remove(item);
                            _BookedTables.Remove(item);
                            break;
                        }
                    }
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Đã xóa!");
                    p.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.InnerException.InnerException.Message);
                }
            });

            BookTableCommand = new RelayCommand<object>(p =>
            {
                if (_isBooked)
                    return false;
                if (string.IsNullOrEmpty(_chiTietDatBan.NguoiDat) || string.IsNullOrEmpty(_chiTietDatBan.SoDienThoai)
                || !_chiTietDatBan.NgayDat.HasValue || _chiTietDatBan.NgayDat == DateTime.MinValue || _chiTietDatBan.NgayDat < DateTime.Now)
                    return false;
                return true;
            }, p =>
            {
                try
                {

                    _chiTietDatBan.MaBan = _maBanAn;
                    _chiTietDatBan.TrangThai = 1;
                    _chiTietDatBan.MaNV = "NV01";
                    int lastIndex = DataProvider.Ins.DB.ChiTietDatBans.Count();
                    string index = DataProvider.Ins.DB.ChiTietDatBans.Count() + 1 < 10
                    ? "0" + lastIndex.ToString()
                    : lastIndex.ToString();
                    _chiTietDatBan.MaDatBan = "DB" + index;
                    DataProvider.Ins.DB.BanAns.ToList().Find(b => b.MaBanAn == _maBanAn && b.TrangThai == "Trống").TrangThai = "Đã đặt";
                    DataProvider.Ins.DB.ChiTietDatBans.Add(_chiTietDatBan);
                    int lastSoHoaDon = DataProvider.Ins.DB.HoaDons.Count();

                    HoaDon hoaDon = new HoaDon() { MaBanAn = _maBanAn, MaNV = _chiTietDatBan.MaNV, SoHoaDon = lastSoHoaDon + 1, NgayLapHoaDon = DateTime.Now, TriGia = 0 };
                    DataProvider.Ins.DB.HoaDons.Add(hoaDon);
                    DataProvider.Ins.DB.SaveChanges();
                    _isBooked = true;
                    OnPropertyChanged("IsBooked");
                    MessageBox.Show("Đã đặt!");
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                }
            });

            ApprovalBookCommand = new RelayCommand<object>(p =>
            {
                return _isBooked;
            }, p =>
            {
                try
                {
                    DataProvider.Ins.DB.BanAns.ToList().Find(b => b.MaBanAn == _maBanAn).TrangThai = "Có khách";
                    DataProvider.Ins.DB.ChiTietDatBans.ToList().FindLast(b => b.MaBan == _maBanAn).TrangThai = 2;
                    DataProvider.Ins.DB.SaveChanges();
                    _isBooked = false;
                    _chiTietDatBan = new ChiTietDatBan();
                    OnPropertyChanged("ChiTietDatBan");
                    OnPropertyChanged("IsBooked");
                    MessageBox.Show("Đã tiếp nhân!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
            });

            CancelBookCommand = new RelayCommand<object>(p =>
            {
                return _isBooked;
            }, p =>
            {
                try
                {
                    DataProvider.Ins.DB.BanAns.ToList().Find(b => b.MaBanAn == _maBanAn).TrangThai = "Trống";
                    DataProvider.Ins.DB.ChiTietDatBans.ToList().Find(b => b.MaBan == _maBanAn).TrangThai = 0;
                    DataProvider.Ins.DB.SaveChanges();
                    _isBooked = false;
                    _chiTietDatBan = new ChiTietDatBan();
                    OnPropertyChanged("IsBooked");
                    OnPropertyChanged("ChiTietDatBan");
                    MessageBox.Show("Đã hủy!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
            });


        }
    }
}
