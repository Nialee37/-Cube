import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'dart:async';
import 'dart:convert';

import '../../API/post_API.dart';
import 'model_widgets.dart';

Future<List<Post>> fetchPost() async {
  final response = await http
      .get(Uri.parse('https://projetcube.tech/Ressource/RessourceAccueil'));

  if (response.statusCode == 200) {
    final parsed = json.decode(response.body).cast<Map<String, dynamic>>();

    return parsed.map<Post>((json) => Post.fromMap(json)).toList();
  } else {
    throw Exception('Failed to load post');
  }
}

class DetailTab extends StatelessWidget {
  const DetailTab({
    required this.id,
    required this.name,
    required this.color,
    Key? key,
  }) : super(key: key);

  final int id;
  final String name;
  final Color color;

//Création du detail de la Card

  Widget _buildBody() {
    return SafeArea(
      bottom: false,
      left: false,
      right: false,
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          Hero(
            tag: id,
            child: AnimatingCard(
              name: name,
              color: color,
              animation: const AlwaysStoppedAnimation(1),
            ),
            flightShuttleBuilder: (context, animation, flightDirection,
                fromHeroContext, toHeroContext) {
              return AnimatingCard(
                name: name,
                color: color,
                animation: animation,
              );
            },
          ),
          Expanded(
            child: const ContainCard(),
          ),
        ],
      ),
    );
  }

//Fonctionnel

  Widget _buildAndroid(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text(name)),
      body: _buildBody(),
    );
  }

//Non tester

  Widget _buildIos(BuildContext context) {
    return CupertinoPageScaffold(
      navigationBar: CupertinoNavigationBar(
        middle: Text(name),
      ),
      child: _buildBody(),
    );
  }

  @override
  Widget build(context) {
    return PlatformWidget(
      androidBuilder: _buildAndroid,
      iosBuilder: _buildIos,
    );
  }
}
