import 'package:flutter/material.dart';
import 'Pages/home_page.dart';
import 'components/appbar.dart';

String email = "";
String mdp = "";

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return const MaterialApp(
      debugShowCheckedModeBanner: false,
      home: Login(),
    );
  }
}

class Login extends StatefulWidget {
  const Login({Key? key}) : super(key: key);

  @override
  _LoginState createState() => _LoginState();
}

class _LoginState extends State<Login> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      resizeToAvoidBottomInset: false,
      backgroundColor: Colors.white,
      appBar: const MainAppBar(
          title: Text(
        "Page de connexion",
        style: TextStyle(
            color: Colors.white, fontWeight: FontWeight.bold, fontSize: 20),
      )),
      body: Padding(
        padding: const EdgeInsets.all(1),
        child: Column(
          children: [
            Padding(
              padding: const EdgeInsets.only(top: 60.0, bottom: 60.0),
              child: Center(
                child: SizedBox(
                    width: 200,
                    height: 150,
                    child: Image.network(
                        'https://cdn.discordapp.com/attachments/907264812231327774/961681705423491122/logoV1.1.png')),
              ),
            ),
            TextField(
              maxLength: 30,
              obscureText: false,
              decoration: const InputDecoration(
                labelText: '  Adresse e-mail',
                labelStyle: TextStyle(color: Color.fromARGB(255, 255, 89, 100)),
                hintText: 'Adresse e-mail',
                focusedBorder: OutlineInputBorder(
                  borderSide: BorderSide(
                      color: Color.fromARGB(255, 255, 89, 100), width: 2.0),
                ),
              ),
              onChanged: (value) => setState(() {
                email = value;
              }),
            ),
            TextField(
              maxLength: 30,
              obscureText: true,
              decoration: const InputDecoration(
                labelText: '  Mot de passe',
                labelStyle: TextStyle(color: Color.fromARGB(255, 255, 89, 100)),
                hintText: 'Mot de passe',
                focusedBorder: OutlineInputBorder(
                  borderSide: BorderSide(
                      color: Color.fromARGB(255, 255, 89, 100), width: 2.0),
                ),
              ),
              onChanged: (value) => setState(() {
                mdp = value;
              }),
            ),
            FlatButton(
              onPressed: () {
                statusIcon = const Icon(Icons.no_accounts);
                favorieIcon = const Icon(Icons.hide_source);
                historiqueIcon = const Icon(Icons.hide_source);
                loginVerif = false;

                Navigator.push(context,
                    MaterialPageRoute(builder: (_) => const CubeAPP()));
              },
              child: const Text(
                'Connectez-vous sans compte',
                style: TextStyle(
                    color: Color.fromARGB(255, 255, 89, 100), fontSize: 15),
              ),
            ),
            Container(
              height: 50,
              width: 250,
              decoration: BoxDecoration(
                  color: const Color.fromARGB(255, 255, 89, 100),
                  borderRadius: BorderRadius.circular(20)),
              child: FlatButton(
                onPressed: () {
                  statusIcon = const Icon(Icons.people);
                  favorieIcon = const Icon(Icons.favorite);
                  historiqueIcon = const Icon(Icons.history);
                  loginVerif = true;

                  Navigator.push(context,
                      MaterialPageRoute(builder: (_) => const CubeAPP()));
                },
                child: const Text(
                  'Se connecter',
                  style: TextStyle(color: Colors.white, fontSize: 25),
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
