import 'dart:math';
import 'package:flutter/material.dart';

//Color Card Test

const _myListOfRandomColors = [
  Colors.red,
  Colors.blue,
  Colors.teal,
  Colors.yellow,
  Colors.amber,
  Colors.deepOrange,
  Colors.green,
  Colors.indigo,
  Colors.lime,
  Colors.pink,
  Colors.orange,
];

final _random = Random();

List<MaterialColor> getRandomColors(int amount) {
  return List<MaterialColor>.generate(amount, (index) {
    return _myListOfRandomColors[_random.nextInt(_myListOfRandomColors.length)];
  });
}

//Titre Card

List<String> cardName(int amount) {
  const Card = <String>[
    "Card1",
    "Card2",
    "Card3",
    "Card4",
    "Card5",
    "Card6",
    "Card7",
    "Card8",
    "Card9",
    "Card10",
    "Card11",
  ];
  return Card;
}
