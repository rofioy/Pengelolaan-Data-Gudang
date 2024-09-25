CREATE DATABASE gudang 
USE gudang;

CREATE TABLE tb_login (
username VARCHAR (20),
pass VARCHAR (10)
);

CREATE TABLE cluster (
alamat VARCHAR (50),
nama VARCHAR (50),
nama_barang VARCHAR (30),
stok VARCHAR (20),
bayar VARCHAR (100)
);

INSERT INTO cluster VALUES ('Purbalingga', 'Hendri', 'Coco Bristel', '700kg', '8400000');

CREATE TABLE karyawan (
no_gudang INT,
gudang VARCHAR (30),
nama VARCHAR (50),
Bon INT
);

INSERT INTO karyawan VALUES ('1', 'Joko', '120kg', '200000');


CREATE TABLE barang (
nama_barang VARCHAR (30),
keluar DATE,
masuk DATE
);

INSERT INTO barang VALUES ('Coco Bristel', '2024-03-12', '2024-03-05');

