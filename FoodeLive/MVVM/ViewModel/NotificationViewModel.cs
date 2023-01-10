using FoodeLive.MVVM.Model;
using FoodeLive.MVVM.View.Windows.CRUD.Notification;
using IT008_DoAnCuoiKi.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FoodeLive.MVVM.ViewModel
{
    public class NotificationViewModel : BaseViewModel
    {
        private CuaHang _cuaHangHoatDong;
        public CuaHang CuaHangHoatDong { get => _cuaHangHoatDong; set { _cuaHangHoatDong = value; OnPropertyChanged(); } }

        private ObservableCollection<ThongBao> _listThongBaoDonHang;
        public ObservableCollection<ThongBao> ListThongBaoDonHang { get => _listThongBaoDonHang; set { _listThongBaoDonHang = value; OnPropertyChanged(); } }
        private ObservableCollection<ThongBao> _listThongBaoDatBan;
        public ObservableCollection<ThongBao> ListThongBaoDatBan { get => _listThongBaoDatBan; set { _listThongBaoDatBan = value; OnPropertyChanged(); } }

        private ThongBao _selectedThongBao;
        public ThongBao SelectedThongBao
        {
            get => _selectedThongBao;
            set
            {
                _selectedThongBao = value; OnPropertyChanged();
                if (_selectedThongBao != null)
                {
                    if (_selectedThongBao.ChiTietDonHang != null)
                    {

                        ConfirmFood confirmFood = new ConfirmFood();
                        confirmFood.ShowDialog();
                    }
                    if(_selectedThongBao.ChiTietDatBan != null)
                    {
                        ConfirmTable confirmTable = new ConfirmTable();
                        confirmTable.ShowDialog();
                    }
                }
            }
        }


        public ICommand ConfirmationFoodCommand { get; set; }
        public ICommand CancelFoodCommand { get; set; }
        public ICommand RefreshFoodCommand { get; set; }
        public ICommand ConfirmationTableCommand { get; set; }

        public NotificationViewModel()
        {
            _selectedThongBao = new ThongBao();
            ConfirmationFoodCommand = new RelayCommand<Window>(p =>
            {
                return _selectedThongBao != null && _selectedThongBao.ChiTietDonHang != null && DataProvider.Ins.DB.ThongBaos.ToList().Find(tb => tb.MaThongBao == _selectedThongBao.MaThongBao)?.ChiTietDonHang.TrangThai == 1;
            }, p =>
            {
                try
                {
                    DataProvider.Ins.DB.ThongBaos.ToList().Find(tb => tb.MaThongBao == _selectedThongBao.MaThongBao).ChiTietDonHang.TrangThai = 2;
                    DataProvider.Ins.DB.SaveChanges();
                    OnPropertyChanged("ListThongBaoDonHang");
                    MessageBox.Show("Đã xác nhận");
                    RefreshFoodCommand.Execute(p);
                    p.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });

            ConfirmationTableCommand = new RelayCommand<Window>(p =>
            {
                return _selectedThongBao != null && _selectedThongBao.ChiTietDatBan != null && DataProvider.Ins.DB.ThongBaos.ToList().Find(tb => tb.MaThongBao == _selectedThongBao.MaThongBao)?.ChiTietDatBan.TrangThai == 0;
            }, p =>
            {
                try
                {
                    DataProvider.Ins.DB.ThongBaos.ToList().Find(tb => tb.MaThongBao == _selectedThongBao.MaThongBao).ChiTietDatBan.TrangThai = 1;
                    DataProvider.Ins.DB.SaveChanges();
                    RefreshFoodCommand.Execute(p);
                    p.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });

            CancelFoodCommand = new RelayCommand<Window>(p =>
            {
                return _selectedThongBao != null && _selectedThongBao.ChiTietDonHang != null && p != null && DataProvider.Ins.DB.ThongBaos.ToList().Find(tb => tb.MaThongBao == _selectedThongBao.MaThongBao)?.ChiTietDonHang.TrangThai <= 1;
            }, p =>
            {
                DataProvider.Ins.DB.ThongBaos.ToList().Find(tb => tb.MaThongBao == _selectedThongBao.MaThongBao).ChiTietDonHang.TrangThai = 5;
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Đã hủy");
                p.Close();
            });
            RefreshFoodCommand = new RelayCommand<object>(p => true, p =>
            {
                _listThongBaoDonHang = new ObservableCollection<ThongBao>(DataProvider.Ins.DB.ThongBaos.Where(t => t.ChiTietDonHang.MonAn.MaCuaHang == _cuaHangHoatDong.MaCuaHang));
                _listThongBaoDatBan = new ObservableCollection<ThongBao>(DataProvider.Ins.DB.ThongBaos.Where(n => n.ChiTietDatBan.BanAn.MaCuaHang == _cuaHangHoatDong.MaCuaHang));
                _selectedThongBao = null;
                OnPropertyChanged("ListThongBaoDonHang");
                OnPropertyChanged("ListThongBaoDatBan");
                OnPropertyChanged("SelectedThongBao");
            }); ;
        }

    }
}
