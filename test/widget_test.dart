import 'package:flutter/cupertino.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:flutter_test/flutter_test.dart';

void main() {
  group('Platform tests', () {
    testWidgets('Builds for Android correctly', (tester) async {
      debugDefaultTargetPlatformOverride = TargetPlatform.android;

      expect(find.byIcon(Icons.menu), findsOneWidget);
      expect(find.byIcon(Icons.refresh), findsOneWidget);
      debugDefaultTargetPlatformOverride = null;
    });

    testWidgets('Builds for iOS correctly', (tester) async {
      debugDefaultTargetPlatformOverride = TargetPlatform.iOS;

      expect(find.byType(CupertinoSliverNavigationBar), findsOneWidget);
      expect(find.byIcon(CupertinoIcons.music_note), findsOneWidget);
      expect(find.byIcon(Icons.menu), findsNothing);

      debugDefaultTargetPlatformOverride = null;
    });
  });
}
