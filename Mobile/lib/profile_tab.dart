import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'widgets.dart';

//Mise en forme

class ProfileTab extends StatelessWidget {
  static const title = 'Profile';
  static const androidIcon = Icon(Icons.person);
  static const iosIcon = Icon(CupertinoIcons.profile_circled);

  const ProfileTab({Key? key}) : super(key: key);

  Widget _buildBody(BuildContext context) {
    return SafeArea(child: LogOutButton());
  }

  Widget _buildAndroid(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text(title),
      ),
      body: _buildBody(context),
    );
  }

  Widget _buildIos(BuildContext context) {
    return CupertinoPageScaffold(
      child: _buildBody(context),
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

//Bouton logout

class LogOutButton extends StatelessWidget {
  static const _logoutMessage = Text("Test Logout Bouton");

  const LogOutButton({Key? key}) : super(key: key);

  Widget _buildAndroid(BuildContext context) {
    return ElevatedButton(
      child: const Text('LOG OUT',
          style: TextStyle(color: Color.fromARGB(255, 255, 255, 255))),
      onPressed: () {
        showDialog<void>(
          context: context,
          builder: (context) {
            return AlertDialog(
              title: const Text('Log out?'),
              content: _logoutMessage,
              actions: [
                TextButton(
                  child: const Text('Got it'),
                  onPressed: () => Navigator.pop(context),
                ),
                TextButton(
                  child: const Text('Cancel'),
                  onPressed: () => Navigator.pop(context),
                ),
              ],
            );
          },
        );
      },
    );
  }

//Bouton logout IOS Non tester

  Widget _buildIos(BuildContext context) {
    return CupertinoButton(
      color: Color.fromARGB(255, 255, 255, 255),
      child: const Text('Log out'),
      onPressed: () {
        showCupertinoModalPopup<void>(
          context: context,
          builder: (context) {
            return CupertinoActionSheet(
              title: const Text('Log out?'),
              message: _logoutMessage,
              actions: [
                CupertinoActionSheetAction(
                  child: const Text('Got it'),
                  onPressed: () => Navigator.pop(context),
                ),
              ],
              cancelButton: CupertinoActionSheetAction(
                child: const Text('Cancel'),
                isDefaultAction: true,
                onPressed: () => Navigator.pop(context),
              ),
            );
          },
        );
      },
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
