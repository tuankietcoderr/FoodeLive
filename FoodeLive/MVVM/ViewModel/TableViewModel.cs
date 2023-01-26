using FoodeLive.MVVM.Model;
using FoodeLive.View.Windows.CRUD.Table;
using IT008_DoAnCuoiKi.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Diagnostics;
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

        private NguoiQuanLy _nguoiQuanLy;
        public NguoiQuanLy NguoiQuanLy { get => _nguoiQuanLy; set { _nguoiQuanLy = value; OnPropertyChanged(); } }

        private NhanVien _nhanVienHoatDong;
        public NhanVien NhanVienHoatDong { get => _nhanVienHoatDong; set { _nhanVienHoatDong = value; OnPropertyChanged(); } }

        private CuaHang _cuaHangHoatDong;
        public CuaHang CuaHangHoatDong { get => _cuaHangHoatDong; set { _cuaHangHoatDong = value; OnPropertyChanged(); } }

        private string _maBanAn;
        public string MaBanAn { get => _maBanAn; set { _maBanAn = value; OnPropertyChanged(); } }
        private string _tenBanAn;
        public string TenBanAn { get => _tenBanAn; set { _tenBanAn = value; OnPropertyChanged(); } }

        private string _loai;
        public string Loai { get { return _loai; } set { _loai = value; OnPropertyChanged(); } }

        private string _maHoaDon;
        public string MaHoaDon { get => _maHoaDon; set { _maHoaDon = value; OnPropertyChanged(); } }

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
                if (DataProvider.Ins.DB.ChiTietDatBans.Count(c => c.MaBanAn == _maBanAn && c.BanAn.TrangThai == "Đã đặt" && _cuaHangHoatDong.MaCuaHang == c.BanAn.MaBanAn) > 0)
                    _chiTietDatBan = DataProvider.Ins.DB.ChiTietDatBans.ToList().Find(c => c.MaBanAn == _maBanAn && c.BanAn.TrangThai == "Đã đặt" && _cuaHangHoatDong.MaCuaHang == c.BanAn.MaBanAn);
                return _chiTietDatBan;
            }
            set { _chiTietDatBan = value; OnPropertyChanged(); }
        }

        private bool _isBooked = false;
        public bool IsBooked
        {
            get
            {
                _isBooked = _cuaHangHoatDong.BanAns.Count(t => t.MaBanAn == _maBanAn && t.TrangThai == "Đã đặt") > 0;
                return _isBooked;
            }
            set { _isBooked = value; OnPropertyChanged(); }
        }

        public ICommand AddTableDialogCommand { get; set; }
        public ICommand AddTableCommand { get; set; }
        public ICommand DeleteTableCommand { get; set; }
        public ICommand BookTableCommand { get; set; }
        public ICommand ApprovalBookCommand { get; set; }
        public ICommand CancelBookCommand { get; set; }

        public TableViewModel()
        {

            _chiTietDatBan = new ChiTietDatBan();


            AddTableDialogCommand = new RelayCommand<object>(p =>
            {
                return _nguoiQuanLy != null;
            }, p =>
            {
                AddTable addTable = new AddTable();
                addTable.ShowDialog();
            });

            // Add table
            AddTableCommand = new RelayCommand<Window>((p) =>
            {
                if (_nguoiQuanLy == null)
                    return false;
                if (string.IsNullOrEmpty(_tenBanAn) || _loai == "Chọn loại bàn")
                    return false;
                return true;
            }, (p) =>
            {
                try
                {
                    if (_ListBanAn.Count == 0)
                        _maBanAn = _cuaHangHoatDong.MaCuaHang + "BA01";
                    else
                    {
                        _maBanAn = _cuaHangHoatDong.MaCuaHang + "BA";
                        var lastBanAn = _ListBanAn.ToList().Last().MaBanAn;
                        int newIndex = Convert.ToInt32(lastBanAn.Substring(_cuaHangHoatDong.MaCuaHang.Length + 2)) + 1;
                        for (int i = 0; i < 8 - _cuaHangHoatDong.MaCuaHang.Length - newIndex.ToString().Length; i++)
                            _maBanAn += "0";
                        _maBanAn += newIndex.ToString();
                    }
                    var banAn = new BanAn() { MaBanAn = _maBanAn, Loai = Loai, TrangThai = "Trống", MaCuaHang = _cuaHangHoatDong.MaCuaHang, TenBanAn = _tenBanAn };
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
                return _maBanAn != string.Empty && _nguoiQuanLy != null;
            },
            (p) =>
            {
                try
                {
                    foreach (BanAn item in _cuaHangHoatDong.BanAns)
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
                if (_isBooked || _nguoiQuanLy == null)
                    return false;
                if (string.IsNullOrEmpty(_chiTietDatBan.NguoiDat) || string.IsNullOrEmpty(_chiTietDatBan.SoDienThoai)
                || !_chiTietDatBan.NgayDat.HasValue || _chiTietDatBan.NgayDat == DateTime.MinValue || _chiTietDatBan.NgayDat < DateTime.Now)
                    return false;
                return true;
            }, p =>
            {
                try
                {
                    if(DataProvider.Ins.DB.BanAns.ToList().Find(b => b.MaBanAn == _maBanAn && b.MaCuaHang == _cuaHangHoatDong.MaCuaHang).TrangThai == "Có khách")
                    {
                        MessageBox.Show("Hiện bàn đang có khách nên không thể đặt bàn!");
                        return;
                    }    
                    _chiTietDatBan.MaBanAn = _maBanAn;
                    _chiTietDatBan.TrangThai = 1;
                    var chiTietDatBans = DataProvider.Ins.DB.ChiTietDatBans.Where(ct => ct.BanAn.MaCuaHang == _cuaHangHoatDong.MaCuaHang).ToList();
                    int cnt = chiTietDatBans.Count();
                    if (cnt > 0)
                    {
                        _chiTietDatBan.MaDatBan = _cuaHangHoatDong.MaCuaHang + "DB";
                        var lastChiTietDatBan = chiTietDatBans.Last();
                        int lastIndex = Convert.ToInt16(lastChiTietDatBan.MaBanAn.Substring(_cuaHangHoatDong.MaCuaHang.Length + 2)) + 1;
                        for (int i = 0; i < 8 - _cuaHangHoatDong.MaCuaHang.Length - lastIndex.ToString().Length; i++)
                            _chiTietDatBan.MaDatBan += "0";
                        _chiTietDatBan.MaDatBan += lastIndex.ToString();
                    }
                    else
                    {
                        _chiTietDatBan.MaDatBan = _cuaHangHoatDong.MaCuaHang + "DB01";
                    }
                    DataProvider.Ins.DB.BanAns.ToList().Find(b => b.MaBanAn == _maBanAn && b.TrangThai == "Trống" && b.MaCuaHang == _cuaHangHoatDong.MaCuaHang).TrangThai = "Đã đặt";
                    DataProvider.Ins.DB.ChiTietDatBans.Add(_chiTietDatBan);
                    int lastMaHoaDon = DataProvider.Ins.DB.HoaDons.Count();
                    // ----------------------- //
                    List<HoaDon> hoaDons = DataProvider.Ins.DB.HoaDons.ToList().Where(t => t.BanAn.MaCuaHang == _cuaHangHoatDong.MaCuaHang).ToList();
                    if (hoaDons.Count == 0)
                        _maHoaDon = _cuaHangHoatDong.MaCuaHang + "HD0001";
                    else
                    {
                        _maHoaDon = _cuaHangHoatDong.MaCuaHang + "HD";
                        HoaDon lastHoaDon = DataProvider.Ins.DB.HoaDons.ToList().Where(t => t.BanAn.MaCuaHang == _cuaHangHoatDong.MaCuaHang).Last();
                        int num = Convert.ToInt16(lastHoaDon.MaHoaDon.Substring(_cuaHangHoatDong.MaCuaHang.Length + 2)) + 1;
                        int lengthOfNum = num.ToString().Length;
                        for (int i = 0; i < 10 - _cuaHangHoatDong.MaCuaHang.Length - lengthOfNum; i++) // Xu li
                            _maHoaDon += "0";
                        _maHoaDon += num.ToString();
                    }
                    HoaDon hoaDon = new HoaDon() { MaBanAn = _maBanAn, MaHoaDon = _maHoaDon ,NgayLapHoaDon = DateTime.Now, TriGia = 0 };
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
                        Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
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
                    DataProvider.Ins.DB.BanAns.ToList().Find(b => b.MaBanAn == _maBanAn && b.MaCuaHang == _cuaHangHoatDong.MaCuaHang).TrangThai = "Có khách";
                    DataProvider.Ins.DB.ChiTietDatBans.ToList().FindLast(b => b.MaBanAn == _maBanAn && b.BanAn.MaCuaHang == _cuaHangHoatDong.MaCuaHang).TrangThai = 2;
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
                    DataProvider.Ins.DB.BanAns.ToList().Find(b => b.MaBanAn == _maBanAn && b.MaCuaHang == _cuaHangHoatDong.MaCuaHang).TrangThai = "Trống";
                    DataProvider.Ins.DB.ChiTietDatBans.ToList().Find(b => b.MaBanAn == _maBanAn && b.BanAn.MaCuaHang == _cuaHangHoatDong.MaCuaHang).TrangThai = 0;
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
