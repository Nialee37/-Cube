import 'package:flutter/material.dart';
import 'package:url_launcher/url_launcher.dart';

import '../api.dart';
import 'profile_page.dart';
import 'favorie_page.dart';
import 'historique_page.dart';

Icon statusIcon = const Icon(Icons.no_accounts);
// Icon favorieIcon = const Icon(Icons.heart_broken);
Icon favorieIcon = const Icon(Icons.hide_source);
Icon historiqueIcon = const Icon(Icons.hide_source);
bool loginVerif = false;

class CubeAPP extends StatefulWidget {
  const CubeAPP({Key? key}) : super(key: key);

  @override
  _CubeAPPState createState() => _CubeAPPState();
}

class _CubeAPPState extends State<CubeAPP> {
  late Future<List<Show>> shows;
  String searchString = "";

  @override
  void initState() {
    super.initState();
    shows = fetchShows();
  }

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Cube APP',
      theme: ThemeData(
        primaryColor: const Color.fromARGB(255, 255, 89, 100),
      ),
      debugShowCheckedModeBanner: false,
      home: Scaffold(
        appBar: AppBar(
          title: const Text('Cube APP'),
          backgroundColor: const Color.fromARGB(255, 255, 89, 100),
          actions: <Widget>[
            IconButton(
              onPressed: () {
                setState(() {
                  if (loginVerif) {
                    Navigator.push(context,
                        MaterialPageRoute(builder: (_) => const ProfilePage()));
                  }
                });
              },
              icon: statusIcon,
            ),
            IconButton(
              onPressed: () {
                setState(() {
                  if (loginVerif) {
                    Navigator.push(context,
                        MaterialPageRoute(builder: (_) => const FavoriePage()));
                  }
                });
              },
              icon: favorieIcon,
            ),
            IconButton(
              onPressed: () {
                setState(() {
                  if (loginVerif) {
                    Navigator.push(
                        context,
                        MaterialPageRoute(
                            builder: (_) => const HistoriquePage()));
                  }
                });
              },
              icon: historiqueIcon,
            ),
          ],
        ),
        body: Column(
          mainAxisSize: MainAxisSize.max,
          mainAxisAlignment: MainAxisAlignment.start,
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            const SizedBox(height: 10),
            Padding(
              padding: const EdgeInsets.symmetric(horizontal: 15.0),
              child: TextField(
                onChanged: (value) {
                  setState(() {
                    searchString = value.toLowerCase();
                  });
                },
                maxLength: 30,
                cursorColor: const Color.fromARGB(255, 255, 89, 100),
                decoration: const InputDecoration(
                  labelText: 'Search',
                  labelStyle:
                      TextStyle(color: Color.fromARGB(255, 255, 89, 100)),
                  suffixIcon: Icon(
                    Icons.search,
                    color: Color.fromARGB(255, 255, 89, 100),
                  ),
                  focusedBorder: OutlineInputBorder(
                    borderSide: BorderSide(
                        color: Color.fromARGB(255, 255, 89, 100), width: 2.0),
                  ),
                ),
              ),
            ),
            const SizedBox(height: 10),
            Expanded(
              child: FutureBuilder(
                builder: (context, AsyncSnapshot<List<Show>> snapshot) {
                  if (snapshot.hasData) {
                    return Center(
                      child: ListView.separated(
                        padding: const EdgeInsets.all(8),
                        itemCount: snapshot.data!.length,
                        itemBuilder: (BuildContext context, int index) {
                          return snapshot.data![index].title
                                  .toLowerCase()
                                  .contains(searchString)
                              ? ListTile(
                                  leading: Builder(
                                    builder: (BuildContext context) {
                                      return IconButton(
                                        iconSize: 49,
                                        icon: Image.network(ImageLogo[
                                            snapshot.data![index].typedoc]),
                                        onPressed: () {
                                          launch(
                                              snapshot.data![index].chemindoc);
                                        },
                                      );
                                    },
                                  ),
                                  title: Text('${snapshot.data?[index].title}'),
                                  subtitle: Text(
                                      'Auteur: ${snapshot.data?[index].auctor}'),
                                )
                              : Container();
                        },
                        separatorBuilder: (BuildContext context, int index) {
                          return snapshot.data![index].title
                                  .toLowerCase()
                                  .contains(searchString)
                              ? const Divider()
                              : Container();
                        },
                      ),
                    );
                  } else if (snapshot.hasError) {
                    return const Center(child: Text('Something went wrong :('));
                  }
                  return const CircularProgressIndicator();
                },
                future: shows,
              ),
            ),
          ],
        ),
      ),
    );
  }
}
