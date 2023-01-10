using FoodeLive.Converter;
using FoodeLive.MVVM.Model;
using FoodeLive.MVVM.View.Windows.CRUD.Setting;
using FoodeLive.View.Windows;
using FoodeLive.View.Windows.Auth;
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
                _tableViewModel = new TableViewModel();
                _foodViewModel = new FoodViewModel();
                _notificationViewModel = new NotificationViewModel();

                _tableViewModel.NguoiQuanLy = _nguoiQuanLy;
                _foodViewModel.NguoiQuanLy = _nguoiQuanLy;
                _ListNhanVien = new ObservableCollection<NhanVien>(_nguoiQuanLy.NhanViens);
            }
        }

        private NhanVien _nhanVienHoatDong;
        public NhanVien NhanVienHoatDong
        {
            get => _nhanVienHoatDong;
            set
            {
                _nhanVienHoatDong = value; OnPropertyChanged();
                _tableViewModel = new TableViewModel();
                _foodViewModel = new FoodViewModel();
                _notificationViewModel = new NotificationViewModel();
                _notificationViewModel.CuaHangHoatDong = _cuaHangHoatDong;
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
                _cuaHangHoatDong = value;
                OnPropertyChanged();
                _cuaHangHoatDong.BanAns = new ObservableCollection<BanAn>(DataProvider.Ins.DB.BanAns.Where(nv => nv.MaCuaHang == _cuaHangHoatDong.MaCuaHang));
                _cuaHangHoatDong.MonAns = new ObservableCollection<MonAn>(DataProvider.Ins.DB.MonAns.Where(nv => nv.MaCuaHang == _cuaHangHoatDong.MaCuaHang));
                _cuaHangHoatDong.NguoiQuanLies = new ObservableCollection<NguoiQuanLy>(DataProvider.Ins.DB.NguoiQuanLies.Where(ql => ql.MaCuaHang == _cuaHangHoatDong.MaCuaHang));

                _foodViewModel.CuaHangHoatDong = _cuaHangHoatDong;
                _tableViewModel.CuaHangHoatDong = _cuaHangHoatDong;
                _notificationViewModel.CuaHangHoatDong = _cuaHangHoatDong;
                _notificationViewModel.ListThongBaoDonHang = new ObservableCollection<ThongBao>(DataProvider.Ins.DB.ThongBaos.Where(t => t.ChiTietDonHang.MonAn.MaCuaHang == _cuaHangHoatDong.MaCuaHang));
                _notificationViewModel.ListThongBaoDatBan = new ObservableCollection<ThongBao>(DataProvider.Ins.DB.ThongBaos.Where(n => n.ChiTietDatBan.BanAn.MaCuaHang == _cuaHangHoatDong.MaCuaHang));
                _tableViewModel.ListBanAn = new ObservableCollection<BanAn>(_cuaHangHoatDong.BanAns);
                _tableViewModel.EmptyTables = new ObservableCollection<BanAn>(_cuaHangHoatDong.BanAns.Where(b => b.TrangThai == "Trống"));
                _tableViewModel.UsingTables = new ObservableCollection<BanAn>(_cuaHangHoatDong.BanAns.Where(b => b.TrangThai == "Có khách"));
                _tableViewModel.BookedTables = new ObservableCollection<BanAn>(_cuaHangHoatDong.BanAns.Where(b => b.TrangThai == "Đã đặt"));
                _foodViewModel.ListMonAn = new ObservableCollection<MonAn>(_cuaHangHoatDong.MonAns);
                _ListHoaDon = new ObservableCollection<HoaDon>(DataProvider.Ins.DB.HoaDons.Where(t => t.BanAn.CuaHang.MaCuaHang == _cuaHangHoatDong.MaCuaHang && t.TrangThai == 1));
            }
        }

        private TableViewModel _tableViewModel;
        public TableViewModel TableViewModel { get => _tableViewModel; set { _tableViewModel = value; OnPropertyChanged(); } }

        private FoodViewModel _foodViewModel;
        public FoodViewModel FoodViewModel { get => _foodViewModel; set { _foodViewModel = value; OnPropertyChanged(); } }

        private NotificationViewModel _notificationViewModel;
        public NotificationViewModel NotificationViewModel { get => _notificationViewModel; set { _notificationViewModel = value; OnPropertyChanged(); } }


        private string _maBanAn;
        public string MaBanAn { get => _maBanAn; set { _maBanAn = value; OnPropertyChanged(); } }

        private string _maHoaDon;
        public string MaHoaDon { get => _maHoaDon; set { _maHoaDon = value; OnPropertyChanged(); } }

        private bool _forceRerender = false;
        public bool ForceRerender { get => _foodViewModel.Ordered || _tableViewModel.IsBooked; set { _forceRerender = value; OnPropertyChanged(); } }

        private ObservableCollection<HoaDon> _ListHoaDon;
        public ObservableCollection<HoaDon> ListHoaDon { get => _ListHoaDon; set { _ListHoaDon = value; OnPropertyChanged(); } }

        private ObservableCollection<NhanVien> _ListNhanVien;
        public ObservableCollection<NhanVien> ListNhanVien
        {
            get
            {
                var temp = new ObservableCollection<NhanVien>(_nguoiQuanLy.NhanViens);
                _ListNhanVien = temp;
                return _ListNhanVien;
            }
            set
            {
                _ListNhanVien = value;
                OnPropertyChanged();
            }
        }


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
                        _maHoaDon = _cuaHangHoatDong.BanAns.ToList().FindLast(b => b.MaBanAn == _maBanAn).HoaDons.ToList().FindLast(hd => hd.MaBanAn == _maBanAn).MaHoaDon;
                    else
                        _maHoaDon = _cuaHangHoatDong.MaCuaHang + "HD0000";
                    _tableViewModel.MaBanAn = _maBanAn;
                    _foodViewModel.MaBanAn = _maBanAn;
                    _foodViewModel.MaHoaDon = _maHoaDon;
                    _tableViewModel.TenBanAn = _selectedItem.TenBanAn;
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
               /* if (_ngayHoaDon.Month != value.Month)
                {
                    _ngayHoaDon = value;
                    OnPropertyChanged();
                    _ListHoaDon = new ObservableCollection<HoaDon>(DataProvider.Ins.DB.HoaDons.Where(b => b.NgayLapHoaDon.Value.Month == _ngayHoaDon.Month && b.BanAn.MaCuaHang == _cuaHangHoatDong.MaCuaHang && b.TrangThai == 1)); OnPropertyChanged("ListHoaDon");
                }*/

                if (_ngayHoaDon.Date != value.Date)
                {
                    _ngayHoaDon = value;
                    OnPropertyChanged();
                    _ListHoaDon = new ObservableCollection<HoaDon>(DataProvider.Ins.DB.HoaDons.Where(b => b.NgayLapHoaDon.Value.Year == _ngayHoaDon.Year &&
                                                                                                            b.NgayLapHoaDon.Value.Month == _ngayHoaDon.Month &&
                                                                                                             b.NgayLapHoaDon.Value.Day == _ngayHoaDon.Day && b.BanAn.MaCuaHang == _cuaHangHoatDong.MaCuaHang && b.TrangThai == 1)); OnPropertyChanged("ListHoaDon");
                }
            }
        }

        public class UserInformation
        {
            public string DiaChi { get; set; }
            public string SoDienThoai { get; set; }
            public string Ten { get; set; }
            public DateTime? NgaySinh { get; set; }
            public string GioiTinh { get; set; }
            public string ImgUrl { get; set; }
            public DateTime? NgayThamGia { get; set; }

        }

        private UserInformation _getUserInformation;
        public UserInformation GetUserInformation
        {
            get
            {
                UserInformation userInformation = new UserInformation();
                if (_nguoiQuanLy != null)
                {
                    NguoiQuanLy nguoiQuanLy = new NguoiQuanLy();
                    nguoiQuanLy = DataProvider.Ins.DB.NguoiQuanLies.ToList().Find(ql => ql.MaQuanLy == _nguoiQuanLy.MaQuanLy);
                    userInformation.NgayThamGia = nguoiQuanLy.NgayThamGia;
                    userInformation.DiaChi = nguoiQuanLy.DiaChi;
                    userInformation.NgaySinh = nguoiQuanLy.NgaySinh;
                    userInformation.GioiTinh = nguoiQuanLy.GioiTinh;
                    userInformation.Ten = nguoiQuanLy.TenQuanLy;
                    userInformation.ImgUrl = nguoiQuanLy.ImgUrl;
                    userInformation.SoDienThoai = nguoiQuanLy.SoDienThoai;
                }
                else
                {
                    NhanVien nhanVien = new NhanVien();
                    nhanVien = DataProvider.Ins.DB.NhanViens.ToList().Find(nv => nv.MaQuanLy == _cuaHangHoatDong.MaQuanLy);
                    userInformation.NgayThamGia = nhanVien.NgayVaoLam;
                    userInformation.DiaChi = nhanVien.DiaChi;
                    userInformation.NgaySinh = nhanVien.NgaySinh;
                    userInformation.GioiTinh = nhanVien.GioiTinh;
                    userInformation.Ten = nhanVien.HoTen;
                    userInformation.ImgUrl = nhanVien.ImgUrl;
                    userInformation.SoDienThoai = nhanVien.SoDienThoai;
                }
                _getUserInformation = userInformation;
                return _getUserInformation;
            }
            set
            {
                _getUserInformation = value;
                OnPropertyChanged();
            }
        }

        private UserInformation _userInformations;
        public UserInformation UserInformations
        {
            get
            {
                _userInformations = _getUserInformation;
                if (_gioiTinhNam == true)
                    _userInformations.GioiTinh = "Nam";
                else
                    _userInformations.GioiTinh = "Nữ";
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
                OnPropertyChanged("UserInformations");
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
                OnPropertyChanged("UserInformations");
                return _gioiTinhNu;
            }
            set
            {
                _gioiTinhNu = value;
                OnPropertyChanged();
            }
        }

        private string _tenCuaHang;
        public string TenCuaHang
        {
            get => DataProvider.Ins.DB.CuaHangs.ToList().Find(ch => ch.MaCuaHang == _cuaHangHoatDong.MaCuaHang).TenCuaHang;
            set { _tenCuaHang = value; OnPropertyChanged(); }
        }

        private string _moTa;
        public string MoTa
        {
            get => DataProvider.Ins.DB.CuaHangs.ToList().Find(ch => ch.MaCuaHang == _cuaHangHoatDong.MaCuaHang).MoTa;
            set { _moTa = value; OnPropertyChanged(); }
        }

        private string _imgUrl;
        public string ImgUrl
        {
            get => DataProvider.Ins.DB.CuaHangs.ToList().Find(ch => ch.MaCuaHang == _cuaHangHoatDong.MaCuaHang).ImgUrl;
            set
            {
                _imgUrl = value;
                OnPropertyChanged();
            }
        }

        private decimal? _luong;
        public decimal? Luong
        {
            get
            {
                if (_nhanVienHoatDong != null)
                    _luong = DataProvider.Ins.DB.NhanViens.ToList().Find(nv => nv.MaQuanLy == _cuaHangHoatDong.MaQuanLy).Luong;
                return _luong;
            }

            set
            {
                _luong = value;
                OnPropertyChanged();
            }
        }

        private NhanVien _selectedNhanVien;
        public NhanVien SelectedNhanVien
        {
            get => _selectedNhanVien;
            set
            {
                _selectedNhanVien = value;
                OnPropertyChanged();
                if (_selectedNhanVien != null)
                {
                    StaffUD staffUD = new StaffUD();
                    staffUD.ShowDialog();
                }
            }
        }

        private ComboBoxItem _reportMonth;
        public ComboBoxItem ReportMonth { get => _reportMonth; set { _reportMonth = value; OnPropertyChanged(); } }

        private ComboBoxItem _reportYear;
        public ComboBoxItem ReportYear { get => _reportYear; set { _reportYear = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _ListTenNhanVien;
        public ObservableCollection<string> ListTenNhanVien
        {
            get
            {
                foreach (var item in _nguoiQuanLy.NhanViens)
                {
                    if (!string.IsNullOrEmpty(item.HoTen))
                        _ListTenNhanVien.Add(item.HoTen.ToLower());
                    if (!string.IsNullOrEmpty(item.MaNV))
                        _ListTenNhanVien.Add(item.MaNV.ToLower());
                    if (!string.IsNullOrEmpty(item.SoDienThoai))
                        _ListTenNhanVien.Add(item.SoDienThoai.ToLower());
                    if (!string.IsNullOrEmpty(item.TenNguoiDung))
                        _ListTenNhanVien.Add(item.TenNguoiDung.ToLower());
                }
                return _ListTenNhanVien;
            }

            set { _ListTenNhanVien = value; OnPropertyChanged(); }
        }

        private ObservableCollection<NhanVien> _searchResultsForStaff;
        public ObservableCollection<NhanVien> SearchResultsForStaff
        {
            get
            {
                _searchResultsForStaff = _ListNhanVien;
                return _searchResultsForStaff;
            }
            set
            {
                _searchResultsForStaff = value;
                OnPropertyChanged();
            }
        }

        public ICommand RefreshCommand { get; set; }
        public ICommand RefreshBillCommand { get; set; }
        public ICommand AnalyzeReportCommand { get; set; }

        public ICommand AddAccountCommand { get; set; }
        public ICommand LogOutCommand { get; set; }

        public ICommand PasswordCommand { get; set; }
        public ICommand UpdateInformationCommand { get; set; }

        public ICommand UpdateInformationDialogCommand { get; set; }
        public ICommand UpdateRestaurantInformationCommand { get; set; }

        public ICommand StaffManagerDialogCommand { get; set; }
        public ICommand UpdateWageCommand { get; set; }
        public ICommand CalculateWageCommand { get; set; }
        public ICommand DeleteAccountCommand { get; set; }
        public ICommand SearchStaffCommand { get; set; }
        public ICommand RefreshStaffCommand { get; set; }



        public MainViewModel()
        {
            _cuaHangHoatDong = new CuaHang();
            _userInformations = new UserInformation();
            _getUserInformation = new UserInformation();
            _ListTenNhanVien = new ObservableCollection<string>();

            RefreshCommand = new RelayCommand<object>(p => true, p =>
            {
                try
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
                    _foodViewModel.SelectedMonAn = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            });

            RefreshBillCommand = new RelayCommand<object>(p => true,
                p =>
                {
                    _ListHoaDon = new ObservableCollection<HoaDon>(DataProvider.Ins.DB.HoaDons.Where(hd => hd.NgayLapHoaDon.Value.Year == _ngayHoaDon.Year &&
                                                                                                            hd.NgayLapHoaDon.Value.Month == _ngayHoaDon.Month &&
                                                                                                             hd.NgayLapHoaDon.Value.Day == _ngayHoaDon.Day && hd.BanAn.MaCuaHang == _cuaHangHoatDong.MaCuaHang && hd.TrangThai == 1));
                    OnPropertyChanged("ListHoaDon");
                }
                );

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

            UpdateInformationCommand = new RelayCommand<Window>(p => true,
            p =>
            {
                try
                {
                    if (_nguoiQuanLy != null)
                    {
                        NguoiQuanLy nguoiQuanLy = new NguoiQuanLy();
                        nguoiQuanLy = DataProvider.Ins.DB.NguoiQuanLies.ToList().Find(ql => ql.MaQuanLy == _nguoiQuanLy.MaQuanLy);
                        nguoiQuanLy.TenQuanLy = _userInformations.Ten;
                        nguoiQuanLy.ImgUrl = _userInformations.ImgUrl;
                        nguoiQuanLy.SoDienThoai = _userInformations.SoDienThoai;
                        nguoiQuanLy.GioiTinh = _userInformations.GioiTinh;
                        nguoiQuanLy.DiaChi = _userInformations.DiaChi;
                        nguoiQuanLy.NgaySinh = _userInformations.NgaySinh;
                    }
                    else
                    {
                        NhanVien nhanVien = new NhanVien();
                        nhanVien = DataProvider.Ins.DB.NhanViens.ToList().Find(nv => nv.MaQuanLy == _cuaHangHoatDong.MaQuanLy);
                        nhanVien.HoTen = _userInformations.Ten;
                        nhanVien.ImgUrl = _userInformations.ImgUrl;
                        nhanVien.DiaChi = _userInformations.DiaChi;
                        nhanVien.SoDienThoai = _userInformations.SoDienThoai;
                        nhanVien.GioiTinh = _userInformations.GioiTinh;
                        nhanVien.NgaySinh = _userInformations.NgaySinh;
                    }
                    DataProvider.Ins.DB.SaveChanges();
                    OnPropertyChanged("GetUserInformation");
                    OnPropertyChanged("NguoiQuanLy");
                    _userInformations = new UserInformation();
                    MessageBox.Show("Cập nhật thành công!");
                    p.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });

            UpdateInformationDialogCommand = new RelayCommand<object>(p => _nguoiQuanLy != null,
                p =>
                {
                    UpdateRestaurant updateRestaurant = new UpdateRestaurant();
                    updateRestaurant.ShowDialog();
                });

            UpdateRestaurantInformationCommand = new RelayCommand<Window>(p =>
            {
                return _nguoiQuanLy != null;
            }, p =>
            {
                try
                {
                    CuaHang cuaHang = new CuaHang();
                    cuaHang = DataProvider.Ins.DB.CuaHangs.ToList().Find(r => r.MaCuaHang == _cuaHangHoatDong.MaCuaHang);
                    cuaHang.TenCuaHang = _tenCuaHang;
                    cuaHang.TenCuaHangKhongDau = VietnameseStringConverter.LocDau(_tenCuaHang);
                    cuaHang.MoTa = _moTa;
                    cuaHang.ImgUrl = _imgUrl;
                    DataProvider.Ins.DB.SaveChanges();
                    OnPropertyChanged("ImgUrl");
                    OnPropertyChanged("TenCuaHang");
                    MessageBox.Show("Cập nhật thành công!");
                    p.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });

            StaffManagerDialogCommand = new RelayCommand<object>(p => _nguoiQuanLy != null,
                p =>
                {
                    try
                    {
                        StaffManager staffManager = new StaffManager();
                        staffManager.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                });

            UpdateWageCommand = new RelayCommand<object>(p => true,
                p =>
                {
                    try
                    {
                        DataProvider.Ins.DB.NhanViens.ToList().Find(nv => nv.MaQuanLy == _nguoiQuanLy.MaQuanLy).Luong = _selectedNhanVien.Luong;
                        DataProvider.Ins.DB.SaveChanges();
                        OnPropertyChanged("SelectedNhanVien");
                        MessageBox.Show("Cập nhật lương thành công!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                });

            CalculateWageCommand = new RelayCommand<object>(p => true,
                p =>
                {
                    DateTime now = DateTime.Now;
                    int tongSoNgayLam = (now - _selectedNhanVien.NgayVaoLam.Value).Days;
                    decimal? tongLuong = _selectedNhanVien.Luong * tongSoNgayLam;
                    MessageBox.Show(tongLuong.Value.ToString());
                });
            DeleteAccountCommand = new RelayCommand<Window>(p => true,
                p =>
                {
                    try
                    {
                        DataProvider.Ins.DB.NhanViens.Remove(_selectedNhanVien);
                        DataProvider.Ins.DB.SaveChanges();
                        MessageBox.Show("Đã xóa!");
                        p.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                });

            SearchStaffCommand = new RelayCommand<string>(p => true, p =>
            {
                try
                {
                    if (string.IsNullOrEmpty(p))
                        return;
                    ObservableCollection<NhanVien> temp = new ObservableCollection<NhanVien>(_nguoiQuanLy.NhanViens.Where(m =>
                    {
                        if (!string.IsNullOrEmpty(m.HoTen))
                            if (VietnameseStringConverter.LocDau(m.HoTen.ToLower()).Contains(VietnameseStringConverter.LocDau(p.ToLower())))
                                return true;
                        if (!string.IsNullOrEmpty(m.MaNV))
                            if (VietnameseStringConverter.LocDau(m.MaNV.ToLower()).Contains(VietnameseStringConverter.LocDau(p.ToLower())))
                                return true;
                        if (!string.IsNullOrEmpty(m.TenNguoiDung))
                            if (VietnameseStringConverter.LocDau(m.TenNguoiDung.ToLower()).Contains(VietnameseStringConverter.LocDau(p.ToLower())))
                                return true;
                        if (!string.IsNullOrEmpty(m.SoDienThoai))
                            if (VietnameseStringConverter.LocDau(m.SoDienThoai.ToLower()).Contains(VietnameseStringConverter.LocDau(p.ToLower())))
                                return true;
                        return false;
                    }));
                    _ListNhanVien = temp;
                    _searchResultsForStaff = _ListNhanVien;
                    OnPropertyChanged("SearchResultsForStaff");
                    OnPropertyChanged("ListNhanVien");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });

            RefreshStaffCommand = new RelayCommand<object>(p => true, p =>
            {
                _ListNhanVien = new ObservableCollection<NhanVien>(_nguoiQuanLy.NhanViens);
                _searchResultsForStaff = _ListNhanVien;
                _selectedNhanVien = null;
                OnPropertyChanged("SearchResultsForStaff");
                OnPropertyChanged("ListNhanVien");
            });
        }
    }
}
