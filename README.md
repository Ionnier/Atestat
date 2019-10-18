# Atestat - Yokai

Codul sursa pentru aplicatia prezentata pentru atestat, repo-ul contine codul sursa C#, serverul de node.js dar si certificatele pentru securizarea serverului.

# Necesitati

Visual Studio

Git

Node.js

npm express

npm mysql

XAMPP


# Instalare

Trebuie creata o baza de date cu urmatoarele setari:

host : 'localhost',

user : 'root',

password : 'rootpassword',

database : 'mysqlcsharp'

table : 'mysqlcsharp'

In tabelul mysqlcsharp trebuie sa exista coloanele id, email, username, password, admin. Fiecare dintre acestea trebuie sa fie unice fiecarui element inserat, insa chiar daca inseram o parola, aceasta nu va merge intrucat trebuie hash-uita de program. Incepem cu crearea unui element in care acesta va fi de tipul:

id, email, username, admin

id = numarul de ordine

email = email-ul user-ului

username- cu acesta ne logam

admin- daca userul este admin sau nu admin => 1 - este admin

                                          0 - nu este admin

Exemplu: 1 myemail@mydomain.com admin 1

Setam serverul de node.js:

Editam server.js astfel incat path-ul certificatului si a cheii corespunde cu fisierele downloadate.

Pentru verificare adaugam rootCA.pem in browser-ul nostru pentru a accesa serverul https

Modificari in aplicatie:

in public Form1() punem in file path-ul catre rootCA.pem

in sendEmail() la linkedresource punem path-ul catre image.jpg

De asemenea deblocham fisierul intrucat visual studio considera fisieul downloadat drept un malicious object de pe Internet si deschidem visual studio cu admin priviligies.

Pentru a functiona al doilea form trebuie ca sa aveam instalat MySqlConnector si creare unei noi baze de date cu:

host : 'localhost',

user : 'root',

password : 'rootpassword',

database : 'pcgarage'

table : 'produs'

In care exista coloane cod_produs, cantitate, pret si tip.
