import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

import '../../widgets.dart';

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
