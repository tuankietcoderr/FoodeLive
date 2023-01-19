BEGIN TRY

BEGIN TRAN;

-- CreateTable
CREATE TABLE [dbo].[BanAn] (
    [MaBanAn] CHAR(10) NOT NULL,
    [Loai] NVARCHAR(6),
    [TrangThai] NVARCHAR(10) CONSTRAINT [DF__BanAn__TrangThai__4AB81AF0] DEFAULT 'N''Trống''',
    [TenBanAn] NVARCHAR(20),
    [MaCuaHang] CHAR(6) NOT NULL,
    CONSTRAINT [PK_MABANAN] PRIMARY KEY CLUSTERED ([MaBanAn])
);

-- CreateTable
CREATE TABLE [dbo].[ChiTietDatBan] (
    [MaDatBan] CHAR(10) NOT NULL,
    [MaBanAn] CHAR(10),
    [NguoiDat] NVARCHAR(30),
    [SoDienThoai] VARCHAR(12),
    [GhiChu] NVARCHAR(200),
    [TrangThai] TINYINT,
    [NgayDat] DATETIME,
    [email] NVARCHAR(1000),
    CONSTRAINT [PK_ChiTietDatBan] PRIMARY KEY CLUSTERED ([MaDatBan])
);

-- CreateTable
CREATE TABLE [dbo].[ChiTietDonHang] (
    [MaDonHang] NVARCHAR(1000) NOT NULL,
    [MaMonAn] CHAR(10) NOT NULL,
    [SoLuong] INT,
    [TrangThai] TINYINT CONSTRAINT [ChiTietDonHang_TrangThai_df] DEFAULT 0,
    CONSTRAINT [PK_SoDonHang_MAMONAN] PRIMARY KEY CLUSTERED ([MaDonHang],[MaMonAn])
);

-- CreateTable
CREATE TABLE [dbo].[ChiTietHoaDon] (
    [MaHoaDon] VARCHAR(12) NOT NULL,
    [MaMonAn] CHAR(10) NOT NULL,
    [SoLuong] INT,
    [TrangThai] TINYINT,
    CONSTRAINT [PK_SoHoaDon_MAMONAN] PRIMARY KEY CLUSTERED ([MaHoaDon],[MaMonAn])
);

-- CreateTable
CREATE TABLE [dbo].[CuaHang] (
    [MaCuaHang] CHAR(6) NOT NULL,
    [MaQuanLy] CHAR(8) NOT NULL,
    [MoTa] NVARCHAR(max),
    [TenCuaHang] NVARCHAR(30),
    [NgayThanhLap] DATETIME,
    [ImgUrl] NVARCHAR(max),
    [DiaChi] NVARCHAR(200),
    [TenCuaHangKhongDau] NVARCHAR(30),
    CONSTRAINT [PK_CuaHang] PRIMARY KEY CLUSTERED ([MaCuaHang])
);

-- CreateTable
CREATE TABLE [dbo].[ThongBao] (
    [MaThongBao] NVARCHAR(1000) NOT NULL,
    [MaDonHang] NVARCHAR(1000),
    [MaMonAn] CHAR(10),
    [MaDatBan] CHAR(10),
    CONSTRAINT [PK_MaThongBao] PRIMARY KEY CLUSTERED ([MaThongBao])
);

-- CreateTable
CREATE TABLE [dbo].[DonHang] (
    [MaDonHang] NVARCHAR(1000) NOT NULL,
    [NgayLapDonHang] DATETIME,
    [TriGia] MONEY,
    [GhiChu] NVARCHAR(50),
    [MaNV] CHAR(10),
    [DiaChi] NVARCHAR(200),
    [TrangThai] TINYINT,
    [email] NVARCHAR(1000) NOT NULL,
    CONSTRAINT [PK_SoDonHang] PRIMARY KEY CLUSTERED ([MaDonHang])
);

-- CreateTable
CREATE TABLE [dbo].[HoaDon] (
    [MaHoaDon] VARCHAR(12) NOT NULL,
    [NgayLapHoaDon] SMALLDATETIME,
    [MaBanAn] CHAR(10),
    [TriGia] MONEY,
    [TrangThai] TINYINT,
    [ThoiGianThanhToan] DATETIME,
    CONSTRAINT [PK_SoHoaDon] PRIMARY KEY CLUSTERED ([MaHoaDon])
);

-- CreateTable
CREATE TABLE [dbo].[MonAn] (
    [MaMonAn] CHAR(10) NOT NULL,
    [TenMonAn] NVARCHAR(60),
    [Gia] MONEY,
    [ImgUrl] NVARCHAR(max),
    [MaCuaHang] CHAR(6) NOT NULL,
    [TenMonAnKhongDau] NVARCHAR(60),
    CONSTRAINT [PK_MAMONAN] PRIMARY KEY CLUSTERED ([MaMonAn])
);

-- CreateTable
CREATE TABLE [dbo].[NguoiQuanLy] (
    [MaQuanLy] CHAR(8) NOT NULL,
    [MaCuaHang] CHAR(6) NOT NULL,
    [TenQuanLy] NVARCHAR(30),
    [SoDienThoai] VARCHAR(12),
    [TenNguoiDung] VARCHAR(20),
    [MatKhau] VARCHAR(30),
    [DiaChi] NVARCHAR(max),
    [NgayThamGia] DATETIME,
    [ImgUrl] NVARCHAR(max),
    [GioiTinh] NVARCHAR(4),
    [NgaySinh] DATETIME,
    CONSTRAINT [PK_NguoiQuanLy] PRIMARY KEY CLUSTERED ([MaQuanLy]),
    CONSTRAINT [UQ__NguoiQua__57E5A81D1FAD7BE8] UNIQUE NONCLUSTERED ([TenNguoiDung])
);

-- CreateTable
CREATE TABLE [dbo].[NhanVien] (
    [MaNV] CHAR(10) NOT NULL,
    [MaCuaHang] CHAR(6) NOT NULL,
    [HoTen] NVARCHAR(40),
    [GioiTinh] NVARCHAR(4),
    [NgaySinh] DATETIME,
    [DiaChi] NVARCHAR(max),
    [Luong] MONEY,
    [SoDienThoai] VARCHAR(12),
    [NgayVaoLam] DATETIME,
    [TenNguoiDung] VARCHAR(20),
    [MatKhau] VARCHAR(30),
    [ImgUrl] NVARCHAR(max),
    [MaQuanLy] CHAR(8) NOT NULL,
    CONSTRAINT [PK_NHANVIEN] PRIMARY KEY CLUSTERED ([MaNV]),
    CONSTRAINT [UQ__NhanVien__57E5A81D2390083C] UNIQUE NONCLUSTERED ([TenNguoiDung])
);

-- CreateTable
CREATE TABLE [dbo].[accounts] (
    [id] NVARCHAR(1000) NOT NULL,
    [user_id] NVARCHAR(1000) NOT NULL,
    [type] NVARCHAR(1000) NOT NULL,
    [provider] NVARCHAR(1000) NOT NULL,
    [provider_account_id] NVARCHAR(1000) NOT NULL,
    [refresh_token] TEXT,
    [access_token] TEXT,
    [expires_at] INT,
    [token_type] NVARCHAR(1000),
    [scope] NVARCHAR(1000),
    [id_token] TEXT,
    [session_state] NVARCHAR(1000),
    CONSTRAINT [accounts_pkey] PRIMARY KEY CLUSTERED ([id]),
    CONSTRAINT [accounts_provider_provider_account_id_key] UNIQUE NONCLUSTERED ([provider],[provider_account_id])
);

-- CreateTable
CREATE TABLE [dbo].[sessions] (
    [id] NVARCHAR(1000) NOT NULL,
    [session_token] NVARCHAR(1000) NOT NULL,
    [user_id] NVARCHAR(1000) NOT NULL,
    [expires] DATETIME2 NOT NULL,
    CONSTRAINT [sessions_pkey] PRIMARY KEY CLUSTERED ([id]),
    CONSTRAINT [sessions_session_token_key] UNIQUE NONCLUSTERED ([session_token])
);

-- CreateTable
CREATE TABLE [dbo].[users] (
    [id] NVARCHAR(1000) NOT NULL,
    [name] NVARCHAR(1000),
    [email] NVARCHAR(1000) NOT NULL,
    [email_verified] DATETIME2,
    [image] NVARCHAR(1000),
    [phone_number] VARCHAR(12),
    [full_name] NVARCHAR(50),
    [address] NVARCHAR(200),
    CONSTRAINT [users_pkey] PRIMARY KEY CLUSTERED ([email]),
    CONSTRAINT [users_id_key] UNIQUE NONCLUSTERED ([id]),
    CONSTRAINT [users_email_key] UNIQUE NONCLUSTERED ([email])
);

-- CreateTable
CREATE TABLE [dbo].[verificationtokens] (
    [identifier] NVARCHAR(1000) NOT NULL,
    [token] NVARCHAR(1000) NOT NULL,
    [expires] DATETIME2 NOT NULL,
    CONSTRAINT [verificationtokens_token_key] UNIQUE NONCLUSTERED ([token]),
    CONSTRAINT [verificationtokens_identifier_token_key] UNIQUE NONCLUSTERED ([identifier],[token])
);

-- CreateTable
CREATE TABLE [dbo].[LikedFood] (
    [MaMonAn] CHAR(10) NOT NULL,
    [email] NVARCHAR(1000) NOT NULL,
    CONSTRAINT [LikedFood_pkey] PRIMARY KEY CLUSTERED ([MaMonAn],[email])
);

-- CreateTable
CREATE TABLE [dbo].[LikedRestaurant] (
    [MaCuaHang] CHAR(6) NOT NULL,
    [email] NVARCHAR(1000) NOT NULL,
    CONSTRAINT [LikedRestaurant_pkey] PRIMARY KEY CLUSTERED ([MaCuaHang],[email])
);

-- AddForeignKey
ALTER TABLE [dbo].[BanAn] ADD CONSTRAINT [FK_MaBanAn_CuaHang] FOREIGN KEY ([MaCuaHang]) REFERENCES [dbo].[CuaHang]([MaCuaHang]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[ChiTietDatBan] ADD CONSTRAINT [FK_ChiTietDatBan_MaBan] FOREIGN KEY ([MaBanAn]) REFERENCES [dbo].[BanAn]([MaBanAn]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[ChiTietDatBan] ADD CONSTRAINT [FK_ChiTietDatBan_NguoiDat] FOREIGN KEY ([email]) REFERENCES [dbo].[users]([email]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[ChiTietDonHang] ADD CONSTRAINT [FK_MAMONAN_ChiTietDonHang] FOREIGN KEY ([MaMonAn]) REFERENCES [dbo].[MonAn]([MaMonAn]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[ChiTietDonHang] ADD CONSTRAINT [FK_SoDonHang] FOREIGN KEY ([MaDonHang]) REFERENCES [dbo].[DonHang]([MaDonHang]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[ChiTietHoaDon] ADD CONSTRAINT [FK_MAMONAN] FOREIGN KEY ([MaMonAn]) REFERENCES [dbo].[MonAn]([MaMonAn]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[ChiTietHoaDon] ADD CONSTRAINT [FK_SoHoaDon] FOREIGN KEY ([MaHoaDon]) REFERENCES [dbo].[HoaDon]([MaHoaDon]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[ThongBao] ADD CONSTRAINT [FK_MaDatBan_ThongBao] FOREIGN KEY ([MaDatBan]) REFERENCES [dbo].[ChiTietDatBan]([MaDatBan]) ON DELETE SET NULL ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[ThongBao] ADD CONSTRAINT [FK_SoDonHang_ThongBao] FOREIGN KEY ([MaDonHang], [MaMonAn]) REFERENCES [dbo].[ChiTietDonHang]([MaDonHang],[MaMonAn]) ON DELETE SET NULL ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[DonHang] ADD CONSTRAINT [FK_EMAIL_DONHANG] FOREIGN KEY ([email]) REFERENCES [dbo].[users]([email]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[DonHang] ADD CONSTRAINT [FK_MANV_DONHANG] FOREIGN KEY ([MaNV]) REFERENCES [dbo].[NhanVien]([MaNV]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[HoaDon] ADD CONSTRAINT [FK_MABANAN] FOREIGN KEY ([MaBanAn]) REFERENCES [dbo].[BanAn]([MaBanAn]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[MonAn] ADD CONSTRAINT [FK_MaMonAn_CuaHang] FOREIGN KEY ([MaCuaHang]) REFERENCES [dbo].[CuaHang]([MaCuaHang]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[NguoiQuanLy] ADD CONSTRAINT [FK_CuaHang] FOREIGN KEY ([MaCuaHang]) REFERENCES [dbo].[CuaHang]([MaCuaHang]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[NhanVien] ADD CONSTRAINT [FK_MaNhanVien_NguoiQuanLy] FOREIGN KEY ([MaQuanLy]) REFERENCES [dbo].[NguoiQuanLy]([MaQuanLy]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[accounts] ADD CONSTRAINT [FK_USER_ID_ACCOUNT] FOREIGN KEY ([user_id]) REFERENCES [dbo].[users]([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[sessions] ADD CONSTRAINT [FK_USER_ID_SESSION] FOREIGN KEY ([user_id]) REFERENCES [dbo].[users]([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[LikedFood] ADD CONSTRAINT [LikedFood_email_fkey] FOREIGN KEY ([email]) REFERENCES [dbo].[users]([email]) ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE [dbo].[LikedFood] ADD CONSTRAINT [LikedFood_MaMonAn_fkey] FOREIGN KEY ([MaMonAn]) REFERENCES [dbo].[MonAn]([MaMonAn]) ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE [dbo].[LikedRestaurant] ADD CONSTRAINT [LikedRestaurant_email_fkey] FOREIGN KEY ([email]) REFERENCES [dbo].[users]([email]) ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE [dbo].[LikedRestaurant] ADD CONSTRAINT [LikedRestaurant_MaCuaHang_fkey] FOREIGN KEY ([MaCuaHang]) REFERENCES [dbo].[CuaHang]([MaCuaHang]) ON DELETE CASCADE ON UPDATE CASCADE;

COMMIT TRAN;

END TRY
BEGIN CATCH

IF @@TRANCOUNT > 0
BEGIN
    ROLLBACK TRAN;
END;
THROW

END CATCH
