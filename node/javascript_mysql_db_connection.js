var mysql = require('mysql');
var express = require('express')
var app = express()
var url = require('url');
var https = require('https')
var fs = require('fs')


var pool = mysql.createPool({
    connectionLimit: 10,
    host: 'localhost',
    user: 'root',
    password: 'rootpassword',
    database: 'mysqlcsharp',
    multipleStatements: true
});

https.createServer({
    key: fs.readFileSync('C:/Users/Dragos/Desktop/CD1/WindowsFormsApp1/node/security/server.key'),
    cert: fs.readFileSync('C:/Users/Dragos/Desktop/CD1/WindowsFormsApp1/node/security/server.crt')
}, app).listen(8080, () => {
    console.log('Listening...')
})

app.get('/', function(req, res) {
    res.send('Hello World');
})



app.get('/searchu', function(req, res) {

    pool.getConnection(function(err, connection) {
        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {

            var q = url.parse(req.url, true);
            var qdata = q.query;

            console.log(qdata.username);

            var searchquerry = "SELECT * FROM mysqlcsharp WHERE `username`= '" + qdata.username + "'";
            console.log(searchquerry);
            connection.query(searchquerry, function(err, result) {
                if (err) throw err;
                console.log("searched")
                console.log(result);
                res.setHeader('Content-Type', 'application/json');
                res.write(JSON.stringify(result))
                res.end()
                connection.release();

            });
        }

    })

})

app.get('/users', function(req, res) {

    pool.getConnection(function(err, connection) {

        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {

            var querry = "SELECT * FROM mysqlcsharp "
            connection.query(querry, function(err, result) {
                if (err) throw err;
                console.log("searched")
                console.log(result);
                res.setHeader('Content-Type', 'application/json');
                res.write(JSON.stringify(result))
                res.end()
                connection.release();

            });
        }

    })


})

app.get('/updateu', function(req, res) {


    pool.getConnection(function(err, connection) {
        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {
            console.log("Connected!");

            var qu = url.parse(req.url, true)
            var qdatau = qu.query;

            var sql = "UPDATE `mysqlcsharp` SET `password`= '" + qdatau.newpassword + "'  WHERE `id`='" + qdatau.id + "' "
            connection.query(sql, function(err, result) {
                if (err) {
                    res.write("Neexecutat")
                    res.end()
                }
                console.log(result.affectedRows + " record(s) updated");
                res.write("Executat")
                res.end()
                connection.release();
            });

        }

    })

})




app.get('/updategriduser', function(req, res) {


    pool.getConnection(function(err, connection) {
        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {
            console.log("Connected!");

            var qu = url.parse(req.url, true)
            var qdatau = qu.query;

            var sql = "UPDATE `mysqlcsharp` SET `username`='" + qdatau.username + "',`email`='" + qdatau.email + "',`admin`='" + qdatau.admin + "' WHERE `id`='" + qdatau.id + "' "
            connection.query(sql, function(err, result) {
                if (err) {
                    res.write("Neexecutat")
                    res.end()
                }
                console.log(result.affectedRows + " record(s) updated");
                res.write("Executat")
                res.end()
                connection.release();
            });

        }

    })

})


app.get('/createu', function(req, res) {


    pool.getConnection(function(err, connection) {
        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {
            console.log("Connected!");

            var qu = url.parse(req.url, true)
            var qdatau = qu.query;
            var sql = "INSERT INTO `mysqlcsharp`(`username`, `password`, `email`, `admin`) VALUES ('" + qdatau.username + "','" + qdatau.password + "','" + qdatau.email + "','0')"
            connection.query(sql, function(err, result) {
                if (err) {
                    res.write("Neexecutat")
                    res.end()
                }
                console.log(result.affectedRows + " record(s) updated");
                res.write("Executat")
                res.end()
                connection.release();
            });

        }

    })

})



app.get('/deletegridu', function(req, res) {


    pool.getConnection(function(err, connection) {
        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {
            console.log("Connected!");

            var qu = url.parse(req.url, true)
            var qdatau = qu.query;
            var sql = "DELETE FROM `mysqlcsharp` WHERE `id`='" + qdatau.id + "'"
            connection.query(sql, function(err, result) {
                if (err) {
                    res.write("Neexecutat")
                    res.end()
                }
                console.log(result.affectedRows + " record(s) updated");
                res.write("Executat")
                res.end()
                connection.release();
            });

        }

    })

})


app.get('/bills', function(req, res) {

    pool.getConnection(function(err, connection) {

        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {

            var querry = "SELECT * FROM facturi "
            connection.query(querry, function(err, result) {
                if (err) throw err;
                console.log("searched")
                console.log(result);
                res.setHeader('Content-Type', 'application/json');
                res.write(JSON.stringify(result))
                res.end()
                connection.release();

            });
        }

    })


})

app.get('/updategridbill', function(req, res) {


    pool.getConnection(function(err, connection) {
        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {
            console.log("Connected!");

            var qu = url.parse(req.url, true)
            var qdatau = qu.query;
            var sql = "UPDATE `facturi` SET `Valoare`='" + qdatau.valoare + "',`Zi`='" + qdatau.zi + "',`Luna`='" + qdatau.luna + "',`An`='" + qdatau.an + "' WHERE `ID`='" + qdatau.id + "' "
            console.log(sql);
            connection.query(sql, function(err, result) {
                if (err) {
                    res.write("Neexecutat")
                    res.end()
                }
                console.log(result.affectedRows + " record(s) updated");
                res.write("Executat")
                res.end()
                connection.release();
            });

        }

    })

})


app.get('/deletegridbill', function(req, res) {


    pool.getConnection(function(err, connection) {
        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {
            console.log("Connected!");

            var qu = url.parse(req.url, true)
            var qdatau = qu.query;
            var sql = "DELETE FROM `facturi` WHERE `id`='" + qdatau.id + "'"
            connection.query(sql, function(err, result) {
                if (err) {
                    res.write("Neexecutat")
                    res.end()
                }
                console.log(result.affectedRows + " record(s) updated");
                res.write("Executat")
                res.end()
                connection.release();
            });

        }

    })

})

app.get('/createbill', function(req, res) {


    pool.getConnection(function(err, connection) {
        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {
            console.log("Connected!");

            var qu = url.parse(req.url, true)
            var qdatau = qu.query;
            var sql = "INSERT INTO `facturi`(`Valoare`, `Zi`, `Luna`, `An`) VALUES ('0','" + qdatau.zi + "','" + qdatau.luna + "','" + qdatau.an + "')"
            connection.query(sql, function(err, result) {
                if (err) {
                    res.write("Neexecutat")
                    res.end()
                }
                console.log(result.affectedRows + " record(s) updated");
                res.write("Executat")
                res.end()
                connection.release();
            });

        }

    })

})

app.get('/products', function(req, res) {

    pool.getConnection(function(err, connection) {

        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {

            var querry = "SELECT * FROM produse "
            connection.query(querry, function(err, result) {
                if (err) throw err;
                console.log("searched")
                console.log(result);
                res.setHeader('Content-Type', 'application/json');
                res.write(JSON.stringify(result))
                res.end()
                connection.release();

            });
        }

    })


})

app.get('/updategridproduct', function(req, res) {


    pool.getConnection(function(err, connection) {
        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {

            console.log("Connected!");
            var qu = url.parse(req.url, true)
            var qdatau = qu.query;
            var sql = "UPDATE `produse` SET `CPret`='" + qdatau.pret + "',`CTip`='" + qdatau.tip + "',`Cantitate`='" + qdatau.cantitate + "' WHERE `ID`='" + qdatau.id + "' "
            console.log(sql);
            connection.query(sql, function(err, result) {
                if (err) {
                    res.write("Neexecutat")
                    res.end()
                }
                console.log(result.affectedRows + " record(s) updated");
                res.write("Executat")
                res.end()
                if (qdatau.modpret == 0) {
                    connection.release();
                }

            });

            if (qdatau.modpret == 1) {
                console.log("pret")
                var sql = "INSERT INTO `pricehistory`(`Produs`, `Pret`, `Date`) VALUES ('" + qdatau.id + "','" + qdatau.pret + "',now())"
                connection.query(sql, function(err, result) {
                    if (err) {
                        res.write("Neexecutat")
                        res.end()
                    }
                    connection.release();
                });
            }


        }

    })

})

app.get('/deletegridproduct', function(req, res) {

    pool.getConnection(function(err, connection) {
        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {
            console.log("Connected!");

            var qu = url.parse(req.url, true)
            var qdatau = qu.query;
            var sql = "DELETE FROM `produse` WHERE `ID`='" + qdatau.id + "'"
            console.log(sql);
            connection.query(sql, function(err, result) {
                if (err) {
                    res.write("Neexecutat")
                    res.end()
                }
                console.log(result.affectedRows + " record(s) updated");
                res.write("Executat")
                res.end()
                connection.release();
            });

        }

    })

    pool.getConnection(function(err, connection) {
        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {
            console.log("Connected!");

            var qu = url.parse(req.url, true)
            var qdatau = qu.query;
            var sql = "DELETE FROM `pricehistory` WHERE `Produs`='" + qdatau.id + "'"
            console.log(sql);
            connection.query(sql, function(err, result) {
                if (err) {
                    res.write("Neexecutat")
                    res.end()
                }
                console.log(result.affectedRows + " record(s) updated");
                connection.release();
            });

        }

    })

    pool.getConnection(function(err, connection) {
        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {
            console.log("Connected!");

            var qu = url.parse(req.url, true)
            var qdatau = qu.query;
            var sql = "DELETE FROM `" + qdatau.tip + "` WHERE `Produs`='" + qdatau.id + "'"
            console.log(sql);
            connection.query(sql, function(err, result) {
                if (err) {
                    res.write("Neexecutat")
                    res.end()
                }
                console.log(result.affectedRows + " record(s) updated");
                connection.release();
            });
        }

    })

})

app.get('/createproduct', function(req, res) {



    pool.getConnection(function(err, connection) {
        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {
            console.log("Connected!");

            var qu = url.parse(req.url, true)
            var qdatau = qu.query;
            var sql = "INSERT INTO `produse`(`Cantitate`, `CTip`, `CPret`) VALUES ('0','0','0')"
            connection.query(sql, function(err, result) {
                if (err) {
                    res.write("Neexecutat")
                    res.end()
                }
                console.log(result.affectedRows + " record(s) updated");
                res.write("Executat")
                res.end()
                connection.release();
            });

        }

    })

})


app.get('/product', function(req, res) {

    pool.getConnection(function(err, connection) {

        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {

            var qu = url.parse(req.url, true)
            var qdatau = qu.query;
            var querry = "SELECT * FROM `pricehistory` WHERE produs='" + qdatau.id + "'"
            connection.query(querry, function(err, result) {
                if (err) throw err;
                console.log("searched")
                console.log(result);
                res.setHeader('Content-Type', 'application/json');
                res.write(JSON.stringify(result))
                res.end()
                connection.release();

            });

        }

    })

})


app.get('/calculator', function(req, res) {

    pool.getConnection(function(err, connection) {

        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {

            var qu = url.parse(req.url, true)
            var qdatau = qu.query;
            var querry = "SELECT * FROM `calculator` WHERE produs='" + qdatau.id + "'"
            connection.query(querry, function(err, result) {
                if (err) throw err;
                console.log("searched")
                console.log(result);
                res.setHeader('Content-Type', 'application/json');
                res.write(JSON.stringify(result))
                res.end()
                connection.release();

            });

        }

    })

})



app.get('/telefon', function(req, res) {

    pool.getConnection(function(err, connection) {

        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {

            var qu = url.parse(req.url, true)
            var qdatau = qu.query;
            var querry = "SELECT * FROM `telefon` WHERE produs='" + qdatau.id + "'"
            connection.query(querry, function(err, result) {
                if (err) throw err;
                console.log("searched")
                console.log(result);
                res.setHeader('Content-Type', 'application/json');
                res.write(JSON.stringify(result))
                res.end()
                connection.release();
            });
        }
    })
})


app.get('/phones', function(req, res) {

    pool.getConnection(function(err, connection) {

        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {

            var querry = "SELECT * FROM telefon "
            connection.query(querry, function(err, result) {
                if (err) throw err;
                console.log("searched")
                console.log(result);
                res.setHeader('Content-Type', 'application/json');
                res.write(JSON.stringify(result))
                res.end()
                connection.release();

            });
        }

    })


})

app.get('/computers', function(req, res) {

    pool.getConnection(function(err, connection) {

        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {

            var querry = "SELECT * FROM calculator "
            connection.query(querry, function(err, result) {
                if (err) throw err;
                console.log("searched")
                console.log(result);
                res.setHeader('Content-Type', 'application/json');
                res.write(JSON.stringify(result))
                res.end()
                connection.release();

            });
        }

    })


})


app.get('/updategridcomputer', function(req, res) {


    pool.getConnection(function(err, connection) {
        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {
            console.log("Connected!");

            var qu = url.parse(req.url, true)
            var qdatau = qu.query;
            var sql = "UPDATE `calculator` SET `CPU`='" + qdatau.cpu + "',`GPU`='" + qdatau.gpu + "',`RAM`='" + qdatau.ram + "',`Stocare`='" + qdatau.stocare + "' WHERE `ID`='" + qdatau.id + "' "
            //UPDATE `calculator` SET `CPU`='abc',`GPU`='abc',`RAM`='abc',`Stocare`='abc' WHERE `ID`='1' 
            //https://localhost:8080/updategridcomputer?cpu=amd&gpu=amd&ram=16&stocare=500&id=1
            console.log(sql);
            connection.query(sql, function(err, result) {
                if (err) {
                    res.write("Neexecutat")
                    res.end()
                }
                console.log(result.affectedRows + " record(s) updated");
                res.write("Executat")
                res.end()
                connection.release();
            });

        }

    })

})


app.get('/updategridphone', function(req, res) {


    pool.getConnection(function(err, connection) {
        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {
            console.log("Connected!");

            var qu = url.parse(req.url, true)
            var qdatau = qu.query;
            var sql = "UPDATE `telefon` SET `Producator`='" + qdatau.producator + "',`Model`='" + qdatau.model + "',`Stocare`='" + qdatau.stocare + "',`RAM`='" + qdatau.ram + "' WHERE `ID`='" + qdatau.id + "' "
            //https://localhost:8080/updategridphone?producator=amd&model=amd&stocare=1000&ram=500&id=2
            //UPDATE `telefon` SET `Producator`='abc',`Model`='abc',`Stocare`='abc',`RAM`='abc' WHERE `ID`='2'
            console.log(sql);
            connection.query(sql, function(err, result) {
                if (err) {
                    res.write("Neexecutat")
                    res.end()
                }
                console.log(result.affectedRows + " record(s) updated");
                res.write("Executat")
                res.end()
                connection.release();
            });

        }

    })

})

/*
app.get('/createfullproduct', function(req, res) {

	var id
	//https://localhost:8080/createfullproduct?pret=500&cantitate=100&tip=Calculator&cpu=Intel&gpu=Nvidia&ram=8&stocare=500
    pool.getConnection(function(err,connection) {
        if (err) {
            res.write("Baza de date offline");
			res.end()
        } else {
            console.log("Connected!");

            var qu = url.parse(req.url, true)
            var qdatau = qu.query;
			console.log("before insert produse")
            var sql="INSERT INTO `produse`(`ID`, `CPret`, `Cantitate`, `CTip`) VALUES ('NULL','"+qdatau.pret+"','"+qdatau.cantitate+"','"+qdatau.tip+"')"
			var sql3="INSERT INTO `calculator`(`ID`, `Produs`, `CPU`, `GPU`, `RAM`, `Stocare`) VALUES ('NULL','"+id+"','"+qdatau.cpu+"','"+qdatau.gpu+"','"+qdatau.ram+"','"+qdatau.stocare+"')"
			console.log(id)
			//INSERT INTO `produse`(`ID`, `CPret`, `Cantitate`, `CTip`) VALUES (NULL,'300','400','Telefon')
			connection.query(sql3, function(err, result) {
                if (err) {
                    res.write("Neexecutat")
                    res.end()
				
                }
				console.log("after insert produse")
				res.write("Executat")
                res.end()
                console.log(result.affectedRows + " record(s) updated");
                console.log(result.insertId);
				id=result.insertId;
				console.log(id)
            });
		console.log("3")
        }
	console.log("before insert produs1e")
    })
	
	    pool.getConnection(function(err,connection) {
        if (err) {
            res.write("Baza de date offline");
			res.end()
        } else {
            console.log("Connected!");

            var qu = url.parse(req.url, true)
            var qdatau = qu.query;
			console.log("before insert pricehistory`")
            var sql="INSERT INTO `pricehistory`(`uselessid`, `Produs`, `Pret`, `Date`) VALUES ('NULL','"+id+"','"+qdatau.pret+"',now())"
			var sql3="INSERT INTO `calculator`(`ID`, `Produs`, `CPU`, `GPU`, `RAM`, `Stocare`) VALUES ('NULL','"+id+"','"+qdatau.cpu+"','"+qdatau.gpu+"','"+qdatau.ram+"','"+qdatau.stocare+"')"
			console.log(id)
			//INSERT INTO `pricehistory`(`uselessid`, `Produs`, `Pret`, `Date`) VALUES (NULL,'produs','500',now())
			connection.query(sql3, function(err, result) {
                if (err) {
                    res.write("Neexecutat")
                    res.end()
                }
				console.log("after insert prh")
                console.log(result.affectedRows + " record(s) updated");
				console.log(id)
                //res.write("Executat")
                //res.end()
				//connection.release();
            });

        }

    })
	
	
	pool.getConnection(function(err,connection) {
        if (err) {
            res.write("Baza de date offline");
			res.end()
        } else {
            console.log("Connected!");

            var qu = url.parse(req.url, true)
            var qdatau = qu.query;
			console.log("before insert calc")
            
			var sql="INSERT INTO `produse`(`ID`, `CPret`, `Cantitate`, `CTip`) VALUES ('NULL','"+qdatau.pret+"','"+qdatau.cantitate+"','"+qdatau.tip+"')"
			//INSERT INTO `calculator`(`ID`, `Produs`, `CPU`, `GPU`, `RAM`, `Stocare`) VALUES (NULL,'2','cpu','gpu','8','500')
			connection.query(sql, function(err, result) {
                if (err) {
                    res.write("Neexecutat")
                    res.end()
                }
				console.log("after insert calc")
                console.log(result.affectedRows + " record(s) updated");
                //res.write("Executat")
                //res.end()
				console.log(id)
				connection.release();
            });


        }

    })


})
*/


app.get('/createfullproduct1', function(req, res) {

    var id;
    pool.getConnection(function(err, connection) {
        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {
            console.log("Connected!");
            var qu = url.parse(req.url, true)
            var qdatau = qu.query;
            console.log("before insert produse")
            var sql = "INSERT INTO `produse`(`ID`, `CPret`, `Cantitate`, `CTip`) VALUES ('NULL','" + qdatau.pret + "','" + qdatau.cantitate + "','" + qdatau.tip + "')"
            //INSERT INTO `produse`(`ID`, `CPret`, `Cantitate`, `CTip`) VALUES (NULL,'300','400','Telefon')
            connection.query(sql, function(err, result) {
                if (err) {
                    res.write("Neexecutat")
                    res.end()
                }
                console.log(result.insertId)              
                id = result.inserId
                res.write("" + result.insertId + "")
                res.end()
				connection.release()
            });

        }
    })


})


app.get('/createfullproduct2', function(req, res) {
	
	
	
    pool.getConnection(function(err, connection) {
        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {
            console.log("Connected!");
            var qu = url.parse(req.url, true)
            var qdatau = qu.query;
            var sql = "INSERT INTO `calculator`(`ID`, `Produs`, `CPU`, `GPU`, `RAM`, `Stocare`) VALUES ('NULL','"+qdatau.id+"','"+qdatau.cpu+"','"+qdatau.gpu+"','"+qdatau.ram+"','"+qdatau.stocare+"')"
            //INSERT INTO `produse`(`ID`, `CPret`, `Cantitate`, `CTip`) VALUES (NULL,'300','400','Telefon')
            console.log(sql)
			connection.query(sql, function(err, result) {
                if (err) {
                    res.write("Neexecutat")
                    res.end()
                }
            });

        }
    })
	
	pool.getConnection(function(err, connection) {
        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {
            console.log("Connected!");
            var qu = url.parse(req.url, true)
            var qdatau = qu.query;
            var sql="INSERT INTO `pricehistory`(`uselessid`, `Produs`, `Pret`, `Date`) VALUES ('NULL','"+qdatau.id+"','"+qdatau.pret+"',now())"
			console.log(sql)
            //INSERT INTO `produse`(`ID`, `CPret`, `Cantitate`, `CTip`) VALUES (NULL,'300','400','Telefon')
            connection.query(sql, function(err, result) {
                if (err) {
                    res.write("Neexecutat")
                    res.end()
                }
				res.write("Executat")
				res.end()
				connection.release()
            });

        }
    })




})



app.get('/createfullphone2', function(req, res) {
	
	
	
    pool.getConnection(function(err, connection) {
        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {
            console.log("Connected!");
            var qu = url.parse(req.url, true)
            var qdatau = qu.query;
            var sql = "INSERT INTO `telefon`(`ID`, `Produs`, `Producator`, `Model`, `Stocare`, `RAM`) VALUES ('NULL','"+qdatau.id+"','"+qdatau.producator+"','"+qdatau.model+"','"+qdatau.stocare+"','"+qdatau.ram+"')"
            //INSERT INTO `telefon`(`ID`, `Produs`, `Producator`, `Model`, `Stocare`, `RAM`) VALUES (NULL,'produs', 'producator','model','stocare','ram')
			//INSERT INTO `produse`(`ID`, `CPret`, `Cantitate`, `CTip`) VALUES (NULL,'300','400','Telefon')
            console.log(sql)
			connection.query(sql, function(err, result) {
                if (err) {
                    res.write("Neexecutat")
                    res.end()
                }
            });

        }
    })
	
	pool.getConnection(function(err, connection) {
        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {
            console.log("Connected!");
            var qu = url.parse(req.url, true)
            var qdatau = qu.query;
            var sql="INSERT INTO `pricehistory`(`uselessid`, `Produs`, `Pret`, `Date`) VALUES ('NULL','"+qdatau.id+"','"+qdatau.pret+"',now())"
			console.log(sql)
            //INSERT INTO `produse`(`ID`, `CPret`, `Cantitate`, `CTip`) VALUES (NULL,'300','400','Telefon')
            connection.query(sql, function(err, result) {
                if (err) {
                    res.write("Neexecutat")
                    res.end()
                }
				res.write("Executat")
				res.end()
				connection.release()
            });

        }
    })




})



app.get('/generatesale', function(req, res) {

    pool.getConnection(function(err, connection) {
        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {
            console.log("Connected!");
            var qu = url.parse(req.url, true)
            var qdatau = qu.query;
            console.log("before insert produse")
            var sql = "INSERT INTO `comenzi`(`ID`, `Data`, `Produs`) VALUES ('NULL', now(),'" + qdatau.produs + "')"
            //INSERT INTO `comenzi`(`uselessid`, `Data`, `Produs`) VALUES (NULL,now(),'2')
            connection.query(sql, function(err, result) {
                if (err) {
                    res.write("Neexecutat")
                    res.end()
                }
                res.write("Executat")
                res.end()
            });

        }
    })
	
	    pool.getConnection(function(err, connection) {
        if (err) {
            
        } else {
            console.log("Connected!");
            var qu = url.parse(req.url, true)
            var qdatau = qu.query;
            console.log("before insert produse")
            var sql = "UPDATE `produse` SET `Cantitate`=`Cantitate`-1 WHERE `ID`='"+qdatau.produs+"'"
            //INSERT INTO `comenzi`(`uselessid`, `Data`, `Produs`) VALUES (NULL,now(),'2')
            connection.query(sql, function(err, result) {
                if (err) {
                }
				connection.release()
            });

        }
    })

//SELECT *, COUNT(Produs) FROM comenzi GROUP BY Produs ORDER BY COUNT(Produs) DESC LIMIT 1 
})


app.get('/getsales', function(req, res) {

    pool.getConnection(function(err, connection) {

        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {

            var qu = url.parse(req.url, true)
            var qdatau = qu.query;
            var querry = "SELECT *, COUNT(Produs) AS COUNT FROM comenzi WHERE Data='"+qdatau.data+"' GROUP BY Produs ORDER BY COUNT(Produs) "
            connection.query(querry, function(err, result) {
                if (err) throw err;
                console.log("searched")
                console.log(result);
                res.setHeader('Content-Type', 'application/json');
                res.write(JSON.stringify(result))
                res.end()
                connection.release();

            });

        }

    })

})



app.get('/getmostsold', function(req, res) {

    pool.getConnection(function(err, connection) {

        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {

            var qu = url.parse(req.url, true)
            var qdatau = qu.query;
            var querry = "SELECT *, COUNT(Produs) FROM comenzi WHERE Data='"+qdatau.data+"' GROUP BY Produs ORDER BY COUNT(Produs) DESC LIMIT 1 "
            connection.query(querry, function(err, result) {
                if (err) throw err;
                console.log("searched")
                console.log(result);
                res.setHeader('Content-Type', 'application/json');
                res.write(JSON.stringify(result))
                res.end()
                connection.release();

            });

        }

    })
// SELECT `Produs`, COUNT(Produs)  from comenzi WHERE Data >='2019-04-12'  <='2019-04-13' GROUP BY Produs ORDER BY COUNT(Produs) DESC
})


app.get('/getsales2', function(req, res) {

    pool.getConnection(function(err, connection) {

        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {

            var qu = url.parse(req.url, true)
            var qdatau = qu.query;
            var querry =  "SELECT `Produs`, COUNT(Produs) AS Numar  from comenzi WHERE Data >='"+qdatau.start+"'  <='"+qdatau.end+"' GROUP BY Produs ORDER BY COUNT(Produs) DESC"
            connection.query(querry, function(err, result) {
                if (err) throw err;
                console.log("searched")
                console.log(result);
                res.setHeader('Content-Type', 'application/json');
                res.write(JSON.stringify(result))
                res.end()
                connection.release();

            });

        }

    })

})


app.get('/getsalesgraph', function(req, res) {

        pool.getConnection(function(err, connection) {

        if (err) {
            res.write("Baza de date offline");
            res.end()
        } else {

            var qu = url.parse(req.url, true)
            var qdatau = qu.query;
            var querry = "SELECT Data, COUNT(Data) AS Counter FROM comenzi WHERE Produs='"+qdatau.product+"' GROUP BY Data "
            connection.query(querry, function(err, result) {
                if (err) throw err;
                console.log("searched")
                console.log(result);
                res.setHeader('Content-Type', 'application/json');
                res.write(JSON.stringify(result))
                res.end()
                connection.release();

            });

        }

    })

})

