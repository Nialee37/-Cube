import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

import 'PAGE/profile_tab.dart';
import 'PAGE/politique_card_tab.dart';
import 'PAGE/finance_card_tab.dart';
import 'PAGE/SecondPage/model_widgets.dart';

void main() => runApp(const MyAdaptingApp());

class MyAdaptingApp extends StatelessWidget {
  const MyAdaptingApp({Key? key}) : super(key: key);

  @override
  Widget build(context) {
    return MaterialApp(
      title: 'Cube',
      theme: ThemeData(
        primarySwatch: Colors.green,
      ),
      darkTheme: ThemeData.light(),
      builder: (context, child) {
        return CupertinoTheme(
          data: const CupertinoThemeData(),
          child: Material(child: child),
        );
      },
      home: const PlatformAdaptingHomePage(),
    );
  }
}

class PlatformAdaptingHomePage extends StatefulWidget {
  const PlatformAdaptingHomePage({Key? key}) : super(key: key);

  @override
  _PlatformAdaptingHomePageState createState() =>
      _PlatformAdaptingHomePageState();
}

class _PlatformAdaptingHomePageState extends State<PlatformAdaptingHomePage> {
  final TabKey = GlobalKey();

  Widget _buildAndroidHomePage(BuildContext context) {
    return PolitiqueTab(
      key: TabKey,
      androidDrawer: _AndroidDrawer(),
    );
  }

  Widget _buildIosHomePage(BuildContext context) {
    return CupertinoTabScaffold(
      tabBar: CupertinoTabBar(
        items: const [
          BottomNavigationBarItem(
            label: PolitiqueTab.title,
            icon: PolitiqueTab.iosIcon,
          ),
          BottomNavigationBarItem(
            label: FinanceTab.title,
            icon: FinanceTab.iosIcon,
          ),
          BottomNavigationBarItem(
            label: ProfileTab.title,
            icon: ProfileTab.iosIcon,
          ),
        ],
      ),
      tabBuilder: (context, index) {
        switch (index) {
          case 0:
            return CupertinoTabView(
              defaultTitle: PolitiqueTab.title,
              builder: (context) => PolitiqueTab(key: TabKey),
            );
          case 1:
            return CupertinoTabView(
              defaultTitle: FinanceTab.title,
              builder: (context) => const FinanceTab(),
            );
          case 2:
            return CupertinoTabView(
              defaultTitle: ProfileTab.title,
              builder: (context) => const ProfileTab(),
            );
          default:
            assert(false, 'Unexpected tab');
            return const SizedBox.shrink();
        }
      },
    );
  }

  @override
  Widget build(context) {
    return PlatformWidget(
      androidBuilder: _buildAndroidHomePage,
      iosBuilder: _buildIosHomePage,
    );
  }
}

class _AndroidDrawer extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Drawer(
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          DrawerHeader(
            decoration: const BoxDecoration(color: Colors.green),
            child: Padding(
              padding: const EdgeInsets.only(bottom: 20),
              child: Icon(
                Icons.account_circle,
                color: Colors.green.shade800,
                size: 96,
              ),
            ),
          ),
          ListTile(
            leading: PolitiqueTab.androidIcon,
            title: const Text(PolitiqueTab.title),
            onTap: () {
              Navigator.pop(context);
            },
          ),
          ListTile(
            leading: FinanceTab.androidIcon,
            title: const Text(FinanceTab.title),
            onTap: () {
              Navigator.pop(context);
              Navigator.push<void>(context,
                  MaterialPageRoute(builder: (context) => const FinanceTab()));
            },
          ),
          const Padding(
            padding: EdgeInsets.symmetric(horizontal: 16),
            child: Divider(),
          ),
          ListTile(
            leading: ProfileTab.androidIcon,
            title: const Text(ProfileTab.title),
            onTap: () {
              Navigator.pop(context);
              Navigator.push<void>(context,
                  MaterialPageRoute(builder: (context) => const ProfileTab()));
            },
          ),
        ],
      ),
    );
  }
}
