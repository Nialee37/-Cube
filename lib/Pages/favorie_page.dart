import 'package:flutter/material.dart';
import 'package:url_launcher/url_launcher.dart';

import '../api.dart';
import '../main.dart';
import 'home_page.dart';

Icon statusIcon = const Icon(Icons.no_accounts);
Icon favorieIcon = const Icon(Icons.hide_source);
Icon historiqueIcon = const Icon(Icons.hide_source);
bool loginVerif = false;

class FavoriePage extends StatefulWidget {
  const FavoriePage({Key? key}) : super(key: key);

  @override
  _FavoriePageState createState() => _FavoriePageState();
}

class _FavoriePageState extends State<FavoriePage> {
  late Future<List<ShowPreference>> showsFavorie;
  String searchString = "";

  @override
  void initState() {
    super.initState();
    showsFavorie = fetchShowsPreference(
        email, mdp, 'https://projetcube.tech/Api/GetFavorisModile');
  }

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Favorie Page',
      theme: ThemeData(
        primaryColor: const Color.fromARGB(255, 255, 89, 100),
      ),
      debugShowCheckedModeBanner: false,
      home: Scaffold(
        appBar: AppBar(
          title: const Text('Favorie Page'),
          backgroundColor: const Color.fromARGB(255, 255, 89, 100),
          actions: <Widget>[
            IconButton(
              onPressed: () {
                setState(() {
                  if (true) {
                    Navigator.push(context,
                        MaterialPageRoute(builder: (_) => const CubeAPP()));
                  }
                });
              },
              icon: const Icon(Icons.home),
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
                builder:
                    (context, AsyncSnapshot<List<ShowPreference>> snapshot) {
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
                future: showsFavorie,
              ),
            ),
          ],
        ),
      ),
    );
  }
}
