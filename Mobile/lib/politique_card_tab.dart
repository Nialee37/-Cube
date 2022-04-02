import 'package:flutter/cupertino.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';

import 'card_detail_tab.dart';
import 'utils.dart';
import 'widgets.dart';

//Renome : PolitiqueTab

class PolitiqueTab extends StatefulWidget {
  static const title = 'politique';
  static const androidIcon = Icon(Icons.monetization_on);
  static const iosIcon = Icon(CupertinoIcons.money_dollar);

  const PolitiqueTab({Key? key, this.androidDrawer}) : super(key: key);

  final Widget? androidDrawer;

  @override
  _PolitiqueTabState createState() => _PolitiqueTabState();
}

class _PolitiqueTabState extends State<PolitiqueTab> {
  static const _itemsLength = 10; //nombre de card

  late List<MaterialColor> colors;
  late List<String> titreCard;

  @override
  void initState() {
    _setData();
    super.initState();
  }

  void _setData() {
    colors = getRandomColors(
        _itemsLength); //temporaire : A remplacer avec le type de document
    titreCard = cardName(
        _itemsLength); //temporaire : A remplacer avec le nom du document
  }

  Widget _listBuilder(BuildContext context, int index) {
    if (index >= _itemsLength) return Container();
    final color = defaultTargetPlatform == TargetPlatform.iOS
        ? colors[index]
        : colors[index].shade400;

    return SafeArea(
      top: false,
      bottom: false,
      child: Hero(
        tag: index,
        child: AnimatingCard(
          name: titreCard[index],
          color: color,
          animation: const AlwaysStoppedAnimation(0),
          onPressed: () => Navigator.of(context).push<void>(
            MaterialPageRoute(
              builder: (context) => DetailTab(
                id: index,
                name: titreCard[index],
                color: color,
              ),
            ),
          ),
        ),
      ),
    );
  }

//Widget And Top Bar Android

  Widget _buildAndroid(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text(PolitiqueTab.title),
      ),
      drawer: widget.androidDrawer,
      body: ListView.builder(
        padding: const EdgeInsets.symmetric(vertical: 12),
        itemCount: _itemsLength,
        itemBuilder: _listBuilder,
      ),
    );
  }

//Widget And Top Bar IOS Non Tester

  Widget _buildIos(BuildContext context) {
    return CustomScrollView(
      slivers: [
        SliverSafeArea(
          top: false,
          sliver: SliverPadding(
            padding: const EdgeInsets.symmetric(vertical: 12),
            sliver: SliverList(
              delegate: SliverChildBuilderDelegate(
                _listBuilder,
                childCount: _itemsLength,
              ),
            ),
          ),
        ),
      ],
    );
  }

//Création Top Bar

  @override
  Widget build(context) {
    return PlatformWidget(
      androidBuilder: _buildAndroid,
      iosBuilder: _buildIos,
    );
  }
}
