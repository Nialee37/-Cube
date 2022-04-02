import 'package:flutter/cupertino.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:flutter_test/flutter_test.dart';

import 'package:platform_design/main.dart';

void main() {
  group('Platform tests', () {
    testWidgets('Builds for Android correctly', (tester) async {
      debugDefaultTargetPlatformOverride = TargetPlatform.android;
      await tester.pumpWidget(const MyAdaptingApp());

      expect(find.byIcon(Icons.menu), findsOneWidget);
      expect(find.byIcon(Icons.refresh), findsOneWidget);
      debugDefaultTargetPlatformOverride = null;
    });

    testWidgets('Builds for iOS correctly', (tester) async {
      debugDefaultTargetPlatformOverride = TargetPlatform.iOS;
      await tester.pumpWidget(const MyAdaptingApp());

      expect(find.byType(CupertinoSliverNavigationBar), findsOneWidget);
      expect(find.byIcon(CupertinoIcons.music_note), findsOneWidget);
      expect(find.byIcon(Icons.menu), findsNothing);

      debugDefaultTargetPlatformOverride = null;
    });
  });
}
