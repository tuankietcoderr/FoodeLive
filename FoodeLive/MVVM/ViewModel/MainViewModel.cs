using FoodeLive.MVVM.Model;
using FoodeLive.Windows;
using FoodeLive.Windows.Auth;
using IT008_DoAnCuoiKi.ViewModel;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static FoodeLive.MVVM.ViewModel.MainViewModel;

namespace FoodeLive.MVVM.ViewModel
{
    public class MainViewModel : BaseViewModel
    {

        private NguoiQuanLy _nguoiQuanLy;
        public NguoiQuanLy NguoiQuanLy
        {
            get => _nguoiQuanLy;
            set
            {
                _nguoiQuanLy = value; OnPropertyChanged();
                _foodViewModel.NguoiQuanLy = _nguoiQuanLy;
                _tableViewModel.NguoiQuanLy = _nguoiQuanLy;
            }
        }

        private NhanVien _nhanVienHoatDong;
        public NhanVien NhanVienHoatDong
        {
            get => _nhanVienHoatDong;
            set
            {
                _nhanVienHoatDong = value; OnPropertyChanged();
                _foodViewModel.NhanVienHoatDong = _nhanVienHoatDong;
                _tableViewModel.NhanVienHoatDong = _nhanVienHoatDong;
            }
        }

        private CuaHang _cuaHangHoatDong;
        public CuaHang CuaHangHoatDong
        {
            get
            {
                if (_nguoiQuanLy == null)
                    return DataProvider.Ins.DB.CuaHangs.ToList().Find(r => r.MaQuanLy == _nhanVienHoatDong.MaQuanLy);
                return DataProvider.Ins.DB.CuaHangs.ToList().Find(r => r.MaQuanLy == _nguoiQuanLy.MaQuanLy);
            }
            set
            {
                _cuaHangHoatDong = value; OnPropertyChanged();
                _cuaHangHoatDong.BanAns = new ObservableCollection<BanAn>(DataProvider.Ins.DB.BanAns.Where(nv => nv.MaCuaHang == _cuaHangHoatDong.MaCuaHang));
                _cuaHangHoatDong.MonAns = new ObservableCollection<MonAn>(DataProvider.Ins.DB.MonAns.Where(nv => nv.MaCuaHang == _cuaHangHoatDong.MaCuaHang));
                _cuaHangHoatDong.NguoiQuanLies = new ObservableCollection<NguoiQuanLy>(DataProvider.Ins.DB.NguoiQuanLies.Where(ql => ql.MaCuaHang == _cuaHangHoatDong.MaCuaHang));

                _tableViewModel = new TableViewModel();
                _foodViewModel = new FoodViewModel();

                _foodViewModel.CuaHangHoatDong = _cuaHangHoatDong;
                _tableViewModel.CuaHangHoatDong = _cuaHangHoatDong;
                _tableViewModel.ListBanAn = new ObservableCollection<BanAn>(_cuaHangHoatDong.BanAns);
                _tableViewModel.EmptyTables = new ObservableCollection<BanAn>(_cuaHangHoatDong.BanAns.Where(b => b.TrangThai == "Trống"));
                _tableViewModel.UsingTables = new ObservableCollection<BanAn>(_cuaHangHoatDong.BanAns.Where(b => b.TrangThai == "Có khách"));
                _tableViewModel.BookedTables = new ObservableCollection<BanAn>(_cuaHangHoatDong.BanAns.Where(b => b.TrangThai == "Đã đặt"));
                _foodViewModel.ListMonAn = new ObservableCollection<MonAn>(_cuaHangHoatDong.MonAns);
                _ListHoaDon = new ObservableCollection<HoaDon>(DataProvider.Ins.DB.HoaDons.Where(t => t.BanAn.CuaHang.MaCuaHang == _cuaHangHoatDong.MaCuaHang));
            }
        }

        private TableViewModel _tableViewModel;
        public TableViewModel TableViewModel { get => _tableViewModel; set { _tableViewModel = value; OnPropertyChanged(); } }

        private FoodViewModel _foodViewModel;
        public FoodViewModel FoodViewModel { get => _foodViewModel; set { _foodViewModel = value; OnPropertyChanged(); } }

        private string _maBanAn;
        public string MaBanAn { get => _maBanAn; set { _maBanAn = value; OnPropertyChanged(); } }

        private int _soHoaDon;
        public int SoHoaDon { get => _soHoaDon; set { _soHoaDon = value; OnPropertyChanged(); } }

        private bool _forceRerender = false;
        public bool ForceRerender { get => _foodViewModel.Ordered || _tableViewModel.IsBooked; set { _forceRerender = value; OnPropertyChanged(); } }

        private ObservableCollection<HoaDon> _ListHoaDon;
        public ObservableCollection<HoaDon> ListHoaDon { get => _ListHoaDon; set { _ListHoaDon = value; OnPropertyChanged(); } }

        private string _currPassword;
        public string CurrPassword { get => _currPassword; set { _currPassword = value; OnPropertyChanged(); } }

        private BanAn _selectedItem;
        public BanAn SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    _maBanAn = _selectedItem.MaBanAn;
                    if (_cuaHangHoatDong.BanAns.ToList().Find(t => t.MaBanAn == _maBanAn).TrangThai == "Có khách")
                        _soHoaDon = _cuaHangHoatDong.BanAns.ToList().FindLast(b => b.MaBanAn == _maBanAn).HoaDons.ToList().FindLast(hd => hd.MaBanAn == _maBanAn).SoHoaDon;
                    else
                        _soHoaDon = 0;
                    _tableViewModel.MaBanAn = _maBanAn;
                    _foodViewModel.MaBanAn = _maBanAn;
                    _foodViewModel.SoHoaDon = _soHoaDon;
                    DetailOrderBook detailOrderBook = new DetailOrderBook();
                    detailOrderBook.ShowDialog();
                }
            }
        }

        private DateTime _ngayHoaDon = DateTime.Now;
        public DateTime NgayHoaDon
        {
            get => _ngayHoaDon;
            set
            {
                if (_ngayHoaDon.Month != value.Month)
                {
                    _ngayHoaDon = value;
                    OnPropertyChanged();
                    _ListHoaDon = new ObservableCollection<HoaDon>(DataProvider.Ins.DB.HoaDons.Where(b => b.NgayLapHoaDon.Value.Month == _ngayHoaDon.Month && b.BanAn.MaCuaHang == _cuaHangHoatDong.MaCuaHang)); OnPropertyChanged("ListHoaDon");
                }
            }
        }

        public class UserInformation
        {
            public string DiaChi { get; set; }
            public string SoDienThoai { get; set; }
            public string Ten { get; set; }
            public string NgaySinh { get; set; }
            public string GioiTinh { get; set; }
            public string ImgUrl { get; set; }
            public string NgayThamGia { get; set; }

        }

        private UserInformation _userInformations;
        public UserInformation UserInformations
        {
            get
            {
                UserInformation userInformation = new UserInformation();
                if (_nguoiQuanLy != null)
                {
                    userInformation.DiaChi = string.Empty;
                    userInformation.SoDienThoai = _nguoiQuanLy.SoDienThoai;
                    DateTime ngayThamGia = _nguoiQuanLy.NgayThamGia.Value;
                    userInformation.NgayThamGia = ngayThamGia.Day + "/" + ngayThamGia.Month + "/" + ngayThamGia.Year;
                    userInformation.GioiTinh = string.Empty;
                    userInformation.Ten = _nguoiQuanLy.TenQuanLy;
                    userInformation.ImgUrl = _nguoiQuanLy.ImgUrl;
                }
                else
                {
                    userInformation.DiaChi = string.Empty;
                    userInformation.SoDienThoai = _nhanVienHoatDong.SoDienThoai;
                    DateTime ngayThamGia = _nhanVienHoatDong.NgayVaoLam.Value;
                    userInformation.NgayThamGia = ngayThamGia.Day + "/" + ngayThamGia.Month + "/" + ngayThamGia.Year;
                    userInformation.GioiTinh = string.Empty;
                    userInformation.Ten = _nhanVienHoatDong.HoTen;
                    userInformation.ImgUrl = _nhanVienHoatDong.ImgUrl;
                }
                _userInformations = userInformation;
                return _userInformations;
            }
            set
            {
                _userInformations = value; OnPropertyChanged();
            }
        }

        private bool _gioiTinhNam = true;
        public bool GioiTinhNam
        {
            get
            {
                return _gioiTinhNam;
            }
            set
            {
                _gioiTinhNam = value;
                OnPropertyChanged();
            }
        }

        private bool _gioiTinhNu = false;
        public bool GioiTinhNu
        {
            get
            {
                return _gioiTinhNu;
            }
            set
            {
                _gioiTinhNu = value;
                OnPropertyChanged();
            }
        }

        private ComboBoxItem _reportMonth;
        public ComboBoxItem ReportMonth { get => _reportMonth; set { _reportMonth = value; OnPropertyChanged(); } }

        private ComboBoxItem _reportYear;
        public ComboBoxItem ReportYear { get => _reportYear; set { _reportYear = value; OnPropertyChanged(); } }

        public ICommand RefreshCommand { get; set; }
        public ICommand AnalyzeReportCommand { get; set; }

        public ICommand AddAccountCommand { get; set; }
        public ICommand LogOutCommand { get; set; }

        public ICommand PasswordCommand { get; set; }
        public ICommand UpdateInformationCommand { get; set; }

        public MainViewModel()
        {
            _cuaHangHoatDong = new CuaHang();
            _userInformations = new UserInformation();
            RefreshCommand = new RelayCommand<object>(p => true, p =>
            {
                _cuaHangHoatDong.BanAns.Clear();
                _cuaHangHoatDong.BanAns = new ObservableCollection<BanAn>(DataProvider.Ins.DB.BanAns.Where(nv => nv.MaCuaHang == _cuaHangHoatDong.MaCuaHang));
                _cuaHangHoatDong.MonAns = new ObservableCollection<MonAn>(DataProvider.Ins.DB.MonAns.Where(nv => nv.MaCuaHang == _cuaHangHoatDong.MaCuaHang));

                _tableViewModel.ListBanAn.Clear();
                _tableViewModel.ListBanAn = new ObservableCollection<BanAn>(_cuaHangHoatDong.BanAns);

                _tableViewModel.EmptyTables.Clear();
                _tableViewModel.EmptyTables = new ObservableCollection<BanAn>(_cuaHangHoatDong.BanAns.Where(b => b.TrangThai == "Trống"));

                _tableViewModel.UsingTables.Clear();
                _tableViewModel.UsingTables = new ObservableCollection<BanAn>(_cuaHangHoatDong.BanAns.Where(b => b.TrangThai == "Có khách"));

                _tableViewModel.BookedTables.Clear();
                _tableViewModel.BookedTables = new ObservableCollection<BanAn>(_cuaHangHoatDong.BanAns.Where(b => b.TrangThai == "Đã đặt"));

                _foodViewModel.ListMonAn.Clear();
                _foodViewModel.ListMonAn = new ObservableCollection<MonAn>(_cuaHangHoatDong.MonAns);

            });
            AnalyzeReportCommand = new RelayCommand<WpfPlot>(p =>
            {
                string monthContent = ReportMonth.Content.ToString();
                string yearContent = ReportYear.Content.ToString();
                return monthContent != "Tháng" && yearContent != "Năm";
            }, p =>
            {

            });
            AddAccountCommand = new RelayCommand<object>(p =>
            {
                return _nguoiQuanLy != null;
            }, p =>
            {
                SignUp signUp = new SignUp();
                signUp.ShowDialog();
            });

            LogOutCommand = new RelayCommand<Window>(p => true,
            p =>
            {
                _cuaHangHoatDong = null;
                _nguoiQuanLy = null;
                _nhanVienHoatDong = null;
                Login login = new Login();
                p.Close();
                login.ShowDialog();
            });

            PasswordCommand = new RelayCommand<object>(p =>
            {
                if (_nguoiQuanLy != null)
                    return !string.IsNullOrEmpty(_currPassword) && _currPassword == _nguoiQuanLy.MatKhau;
                return !string.IsNullOrEmpty(_currPassword) && _currPassword == _nhanVienHoatDong.MatKhau;
            },
            p =>
            {

            });
        }
    }
}
