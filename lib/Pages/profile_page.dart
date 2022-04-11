import 'dart:async';
import 'package:flutter/material.dart';

import '../api.dart';
import '../components/appbar.dart';
import '../main.dart';

class ProfilePage extends StatefulWidget {
  const ProfilePage({Key? key}) : super(key: key);
  @override
  _ProfilePageState createState() => _ProfilePageState();
}

class _ProfilePageState extends State<ProfilePage> {
  late Future<LoginData> showsbis;

  @override
  void initState() {
    super.initState();
    showsbis = fetchShowsLogin(email, mdp);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: const MainAppBar(
          title: Text(
        "Profile",
        style: TextStyle(
            color: Colors.white, fontWeight: FontWeight.bold, fontSize: 20),
      )),
      body: FutureBuilder(
        builder: (context, AsyncSnapshot<LoginData> snapshot) {
          if (snapshot.hasData) {
            return Column(
              children: [
                const Padding(padding: EdgeInsets.only(bottom: 20)),
                buildUserInfoDisplay('${snapshot.data?.name}', 'Nom et prénom'),
                buildUserInfoDisplay(
                    '${snapshot.data?.dateNaissance}', 'Date de naissance'),
                buildUserInfoDisplay('${snapshot.data?.genre}', 'Genre'),
                buildUserInfoDisplay(
                    '${snapshot.data?.adressemail}', 'Adresse e-mail'),
                buildUserInfoDisplay('${snapshot.data?.adresse}', 'Adresse'),
              ],
            );
          } else if (snapshot.hasError) {
            return const Center(child: Text('Something went wrong :('));
          }
          return const Center(child: CircularProgressIndicator());
        },
        future: showsbis,
      ),
    );
  }

  Widget buildUserInfoDisplay(String getValue, String title) => Padding(
      padding: const EdgeInsets.only(bottom: 10),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            title,
            style: const TextStyle(
              fontSize: 15,
              fontWeight: FontWeight.w500,
              color: Colors.grey,
            ),
          ),
          Container(
            width: 350,
            height: 40,
            decoration: const BoxDecoration(
                border: Border(
                    bottom: BorderSide(
              color: Colors.grey,
              width: 1,
            ))),
            child: TextButton(
                onPressed: () {},
                child: Text(
                  getValue,
                  style: const TextStyle(
                    fontSize: 16,
                    height: 1.4,
                    color: Color.fromARGB(255, 255, 89, 100),
                  ),
                )),
          )
        ],
      ));
}
