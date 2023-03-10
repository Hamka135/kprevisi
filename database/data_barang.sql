-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Waktu pembuatan: 10 Mar 2023 pada 16.57
-- Versi server: 10.4.25-MariaDB
-- Versi PHP: 8.0.23

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `data_barang`
--

-- --------------------------------------------------------

--
-- Struktur dari tabel `barang`
--

CREATE TABLE `barang` (
  `IdBrg` char(8) NOT NULL,
  `NamaBarang` varchar(20) NOT NULL,
  `Stok` int(13) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `barang`
--

INSERT INTO `barang` (`IdBrg`, `NamaBarang`, `Stok`) VALUES
('BRG001', 'Barang 1', 19),
('BRG002', 'Barang 2', 197),
('BRG003', 'Barang 3', 200),
('BRG004', 'Mouse', 4),
('BRG005', 'Kertas HVS A4', 3),
('BRG006', 'printer', 50),
('BRG007', 'Lem', 2);

-- --------------------------------------------------------

--
-- Struktur dari tabel `brg_keluar`
--

CREATE TABLE `brg_keluar` (
  `IdBKeluar` char(8) NOT NULL,
  `IdBrg` char(8) NOT NULL,
  `IdDivisi` char(8) NOT NULL,
  `NamaBarang` varchar(20) NOT NULL,
  `NamaDivisi` varchar(50) NOT NULL,
  `Jumlah` int(12) NOT NULL,
  `Tanggal` varchar(15) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `brg_keluar`
--

INSERT INTO `brg_keluar` (`IdBKeluar`, `IdBrg`, `IdDivisi`, `NamaBarang`, `NamaDivisi`, `Jumlah`, `Tanggal`) VALUES
('BK001', 'BRG001', 'DVS001', 'Barang 1', 'ANSIE', 3, '2/10/2023'),
('BK002', 'BRG002', 'DVS001', 'Barang 2', 'ANSIE', 1, '2/10/2023'),
('BK003', 'BRG002', 'DVS002', 'Barang 2', 'RDC', 1, '2/10/2023'),
('BK004', 'BRG002', 'DVS003', 'Barang 2', 'Tata Usaha', 2, '2/13/2023'),
('BK005', 'BRG005', 'DVS001', 'Kertas HVS A4', 'ANSIE', 2, '2/17/2023');

--
-- Trigger `brg_keluar`
--
DELIMITER $$
CREATE TRIGGER `delete_keluar` AFTER DELETE ON `brg_keluar` FOR EACH ROW BEGIN
    UPDATE barang SET Stok = Stok + OLD.Jumlah
    WHERE IdBrg = OLD.IdBrg;
END
$$
DELIMITER ;
DELIMITER $$
CREATE TRIGGER `t_keluar` AFTER INSERT ON `brg_keluar` FOR EACH ROW BEGIN
    UPDATE barang SET Stok = Stok - NEW.Jumlah
    WHERE IdBrg = NEW.IdBrg;
END
$$
DELIMITER ;
DELIMITER $$
CREATE TRIGGER `up_keluar` AFTER UPDATE ON `brg_keluar` FOR EACH ROW BEGIN
    UPDATE barang SET Stok = (Stok + OLD.Jumlah)-NEW.Jumlah
    WHERE IdBrg = NEW.IdBrg;
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Struktur dari tabel `brg_masuk`
--

CREATE TABLE `brg_masuk` (
  `IdBMasuk` char(8) NOT NULL,
  `IdBrg` char(8) NOT NULL,
  `IdSupplier` char(8) NOT NULL,
  `NamaBarang` varchar(20) NOT NULL,
  `NamaSupplier` varchar(25) NOT NULL,
  `Jumlah` int(12) NOT NULL,
  `Tanggal` varchar(15) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `brg_masuk`
--

INSERT INTO `brg_masuk` (`IdBMasuk`, `IdBrg`, `IdSupplier`, `NamaBarang`, `NamaSupplier`, `Jumlah`, `Tanggal`) VALUES
('BM001', 'BRG001', 'SPL001', 'Barang 1', 'PT Sinar Jaya', 4, '2/9/2023'),
('BM002', 'BRG002', 'SPL001', 'Barang 2', 'PT Sinar Jaya', 10, '2/2/2023'),
('BM003', 'BRG003', 'SPL001', 'Barang 3', 'PT Sinar Jaya', 6, '2/10/2023'),
('BM004', 'BRG002', 'SPL001', 'Barang 2', 'PT Sinar Jaya', 2, '2/10/2023'),
('BM005', 'BRG002', 'SPL001', 'Barang 2', 'PT Sinar Jaya', 1, '2/10/2023');

-- --------------------------------------------------------

--
-- Struktur dari tabel `divisi`
--

CREATE TABLE `divisi` (
  `IdDivisi` char(8) NOT NULL,
  `NamaDivisi` varchar(50) NOT NULL,
  `NamaKaryawan` varchar(50) NOT NULL,
  `Kontak` varchar(13) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `divisi`
--

INSERT INTO `divisi` (`IdDivisi`, `NamaDivisi`, `NamaKaryawan`, `Kontak`) VALUES
('DVS001', 'ANSIE', 'Ruli Kurniawan', '085872246357'),
('DVS002', 'RDC', 'Irfanuddin', '087782889299'),
('DVS003', 'Tata Usaha', 'Purwani Sekar', '081294961955'),
('DVS004', 'Umum', 'Awang Abdullah', '088754632558');

-- --------------------------------------------------------

--
-- Struktur dari tabel `login`
--

CREATE TABLE `login` (
  `userId` char(8) NOT NULL,
  `username` varchar(30) NOT NULL,
  `Level` varchar(20) NOT NULL,
  `password` varchar(13) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `login`
--

INSERT INTO `login` (`userId`, `username`, `Level`, `password`) VALUES
('USR001', 'hamka', 'Administrator', 'hamkaotosaka'),
('USR002', 'batara', 'Karyawan Umum', 'batarabatara'),
('USR003', 'alvin', 'Karyawan Umum', '123'),
('USR004', 'a', 'Administrator', 'a');

-- --------------------------------------------------------

--
-- Struktur dari tabel `supplier`
--

CREATE TABLE `supplier` (
  `IdSupplier` char(8) NOT NULL,
  `Namasupplier` varchar(30) NOT NULL,
  `Kontak` varchar(13) NOT NULL,
  `Alamat` varchar(25) NOT NULL,
  `Kota` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `supplier`
--

INSERT INTO `supplier` (`IdSupplier`, `Namasupplier`, `Kontak`, `Alamat`, `Kota`) VALUES
('SPL001', 'PT Sinar Jaya', '081225884299', 'Jl. Kalibata City No. 70', 'Jakarta Selatan'),
('SPL002', 'PT Pemana Indonesia', '02155429988', 'Jl. i gusti ngurah rai No', 'Depok');

--
-- Indexes for dumped tables
--

--
-- Indeks untuk tabel `barang`
--
ALTER TABLE `barang`
  ADD PRIMARY KEY (`IdBrg`);

--
-- Indeks untuk tabel `brg_keluar`
--
ALTER TABLE `brg_keluar`
  ADD PRIMARY KEY (`IdBKeluar`),
  ADD KEY `IdBrg` (`IdBrg`),
  ADD KEY `IdDivisi` (`IdDivisi`);

--
-- Indeks untuk tabel `brg_masuk`
--
ALTER TABLE `brg_masuk`
  ADD PRIMARY KEY (`IdBMasuk`),
  ADD KEY `IdBrg` (`IdBrg`,`IdSupplier`),
  ADD KEY `IdSupplier` (`IdSupplier`);

--
-- Indeks untuk tabel `divisi`
--
ALTER TABLE `divisi`
  ADD PRIMARY KEY (`IdDivisi`);

--
-- Indeks untuk tabel `login`
--
ALTER TABLE `login`
  ADD PRIMARY KEY (`userId`);

--
-- Indeks untuk tabel `supplier`
--
ALTER TABLE `supplier`
  ADD PRIMARY KEY (`IdSupplier`);

--
-- Ketidakleluasaan untuk tabel pelimpahan (Dumped Tables)
--

--
-- Ketidakleluasaan untuk tabel `brg_keluar`
--
ALTER TABLE `brg_keluar`
  ADD CONSTRAINT `brg_keluar_ibfk_1` FOREIGN KEY (`IdBrg`) REFERENCES `barang` (`IdBrg`),
  ADD CONSTRAINT `brg_keluar_ibfk_2` FOREIGN KEY (`IdDivisi`) REFERENCES `divisi` (`IdDivisi`);

--
-- Ketidakleluasaan untuk tabel `brg_masuk`
--
ALTER TABLE `brg_masuk`
  ADD CONSTRAINT `brg_masuk_ibfk_1` FOREIGN KEY (`IdSupplier`) REFERENCES `supplier` (`IdSupplier`),
  ADD CONSTRAINT `brg_masuk_ibfk_2` FOREIGN KEY (`IdBrg`) REFERENCES `barang` (`IdBrg`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
