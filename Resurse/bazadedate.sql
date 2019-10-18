-- phpMyAdmin SQL Dump
-- version 4.8.3
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 18, 2019 at 01:19 PM
-- Server version: 10.1.37-MariaDB
-- PHP Version: 7.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `mysqlcsharp`
--

-- --------------------------------------------------------

--
-- Table structure for table `calculator`
--

CREATE TABLE `calculator` (
  `ID` int(50) NOT NULL,
  `Produs` int(50) NOT NULL,
  `CPU` varchar(50) NOT NULL,
  `GPU` varchar(50) NOT NULL,
  `RAM` int(50) NOT NULL,
  `Stocare` int(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `calculator`
--

INSERT INTO `calculator` (`ID`, `Produs`, `CPU`, `GPU`, `RAM`, `Stocare`) VALUES
(1, 2, 'Intel', 'Nvidia', 16, 500),
(2, 3, 'Nvidia', 'AmD', 8, 1000);

-- --------------------------------------------------------

--
-- Table structure for table `comenzi`
--

CREATE TABLE `comenzi` (
  `ID` int(20) NOT NULL,
  `Data` date NOT NULL,
  `Produs` int(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `comenzi`
--

INSERT INTO `comenzi` (`ID`, `Data`, `Produs`) VALUES
(1, '2019-04-12', 2),
(4, '2019-04-12', 2),
(9, '2019-04-12', 2),
(17, '2019-04-13', 2),
(19, '2019-04-13', 2),
(26, '2019-04-17', 2),
(28, '2019-04-15', 2),
(30, '2019-04-15', 2),
(32, '2019-04-15', 2),
(35, '2019-04-15', 1),
(36, '2019-04-15', 2),
(37, '2019-04-15', 1),
(38, '2019-04-15', 2),
(39, '2019-04-15', 1),
(40, '2019-04-15', 2),
(41, '2019-04-15', 1),
(42, '2019-04-15', 2),
(44, '2019-04-16', 2),
(45, '2019-04-16', 1),
(46, '2019-04-16', 2);

-- --------------------------------------------------------

--
-- Table structure for table `facturi`
--

CREATE TABLE `facturi` (
  `ID` int(50) NOT NULL,
  `Valoare` int(50) NOT NULL,
  `Zi` int(50) NOT NULL,
  `Luna` int(50) NOT NULL,
  `An` int(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `facturi`
--

INSERT INTO `facturi` (`ID`, `Valoare`, `Zi`, `Luna`, `An`) VALUES
(2, 130, 5, 6, 2017),
(25, 90, 5, 4, 2019),
(26, 23, 5, 4, 2019),
(27, 43, 5, 4, 2019),
(28, 6336, 15, 4, 2019);

-- --------------------------------------------------------

--
-- Table structure for table `mysqlcsharp`
--

CREATE TABLE `mysqlcsharp` (
  `id` int(10) NOT NULL,
  `username` text NOT NULL,
  `password` text NOT NULL,
  `email` text NOT NULL,
  `admin` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `mysqlcsharp`
--

INSERT INTO `mysqlcsharp` (`id`, `username`, `password`, `email`, `admin`) VALUES
(1, 'admin', '$MYHASH$V1$10000$tXa1pgtwWcjk03QgvaNTrD0f/JY9KnWkPQDetre2TLrqudar', 'dragosbahrim@outlook.com', 1),
(4, 'user', '$MYHASH$V1$10000$SaF96KekQeCOq5r3T20YR6CkMleYHaBSyqoe/nFOCFsj8qwc', 'cosminzeed@gmail.com', 0);

-- --------------------------------------------------------

--
-- Table structure for table `pricehistory`
--

CREATE TABLE `pricehistory` (
  `uselessid` int(5) NOT NULL,
  `Produs` int(11) NOT NULL,
  `Pret` int(11) NOT NULL,
  `Date` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `pricehistory`
--

INSERT INTO `pricehistory` (`uselessid`, `Produs`, `Pret`, `Date`) VALUES
(1, 1, 450, '2019-04-15 12:27:48'),
(2, 1, 500, '2019-04-15 12:27:51'),
(3, 1, 600, '2019-04-15 12:27:54'),
(4, 1, 600, '2019-04-15 12:40:28'),
(5, 2, 400, '2019-04-15 12:40:31'),
(6, 1, 600, '2019-04-15 14:19:15'),
(7, 1, 600, '2019-04-15 14:19:20'),
(8, 3, 500, '2019-04-16 08:44:56');

-- --------------------------------------------------------

--
-- Table structure for table `produse`
--

CREATE TABLE `produse` (
  `ID` int(50) NOT NULL,
  `CPret` int(11) NOT NULL,
  `Cantitate` int(30) NOT NULL,
  `CTip` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `produse`
--

INSERT INTO `produse` (`ID`, `CPret`, `Cantitate`, `CTip`) VALUES
(1, 600, 82, 'Telefon'),
(2, 400, 38, 'Calculator'),
(3, 500, 300, 'Calculator');

-- --------------------------------------------------------

--
-- Table structure for table `telefon`
--

CREATE TABLE `telefon` (
  `ID` int(20) NOT NULL,
  `Produs` int(20) NOT NULL,
  `Producator` text NOT NULL,
  `Model` text NOT NULL,
  `Stocare` int(20) NOT NULL,
  `RAM` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `telefon`
--

INSERT INTO `telefon` (`ID`, `Produs`, `Producator`, `Model`, `Stocare`, `RAM`) VALUES
(1, 1, 'Xiaomi', 'MI Mix 2', 128, 8);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `calculator`
--
ALTER TABLE `calculator`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `comenzi`
--
ALTER TABLE `comenzi`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `facturi`
--
ALTER TABLE `facturi`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `ID` (`ID`);

--
-- Indexes for table `mysqlcsharp`
--
ALTER TABLE `mysqlcsharp`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `pricehistory`
--
ALTER TABLE `pricehistory`
  ADD PRIMARY KEY (`uselessid`);

--
-- Indexes for table `produse`
--
ALTER TABLE `produse`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `ID` (`ID`);

--
-- Indexes for table `telefon`
--
ALTER TABLE `telefon`
  ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `calculator`
--
ALTER TABLE `calculator`
  MODIFY `ID` int(50) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `comenzi`
--
ALTER TABLE `comenzi`
  MODIFY `ID` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=47;

--
-- AUTO_INCREMENT for table `facturi`
--
ALTER TABLE `facturi`
  MODIFY `ID` int(50) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;

--
-- AUTO_INCREMENT for table `mysqlcsharp`
--
ALTER TABLE `mysqlcsharp`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `pricehistory`
--
ALTER TABLE `pricehistory`
  MODIFY `uselessid` int(5) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `produse`
--
ALTER TABLE `produse`
  MODIFY `ID` int(50) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `telefon`
--
ALTER TABLE `telefon`
  MODIFY `ID` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
