import 'dart:convert';
import 'package:http/http.dart' as http;

const List ImageLogo = [
  "https://cdn.discordapp.com/attachments/444945070873903116/960960727873646622/unknown.png",
  "https://cdn.discordapp.com/attachments/444945070873903116/960960727873646622/unknown.png",
  "https://cdn.discordapp.com/attachments/907264812231327774/961313880569700352/Excel.png",
  "https://cdn.discordapp.com/attachments/907264812231327774/961313880372559932/Word.png",
  "https://cdn.discordapp.com/attachments/907264812231327774/961313895899869284/PDF.png",
  "https://cdn.discordapp.com/attachments/907264812231327774/961313896130564156/Image.png",
  "https://cdn.discordapp.com/attachments/907264812231327774/961313880125079562/Video.png",
];

//Login

Future<LoginData> fetchShowsLogin(String email, String motdepasse) async {
  final response = await http.post(
      Uri.parse('https://projetcube.tech/Api/LoginfromMobile'),
      body: {"email": email, "motdepasse": motdepasse});
  if (response.statusCode == 200) {
    var login = jsonDecode(response.body);
    return LoginData.fromJson(login);
  } else {
    throw Exception('Failed to load shows');
  }
}

class LoginData {
  final int id;
  final String name;
  final String dateNaissance;
  final int genre;
  final String roles;
  final String adressemail;
  final String adresse;

  LoginData({
    required this.id,
    required this.name,
    required this.dateNaissance,
    required this.genre,
    required this.roles,
    required this.adressemail,
    required this.adresse,
  });

  factory LoginData.fromJson(Map<String, dynamic> json) {
    return LoginData(
      id: json['id'],
      name: json['nom'] + " " + json['prenom'],
      dateNaissance: json['dateNaissance'],
      genre: json['genre'],
      roles: json['roles']['libelle'],
      adressemail: json['mail'],
      adresse: json['adresse']['numero'] +
          " " +
          json['adresse']['nom'] +
          ", " +
          json['adresse']['ville']['cPostal'] +
          " " +
          json['adresse']['ville']['nom'],
    );
  }
}

//List ressources

Future<List<Show>> fetchShows() async {
  final response =
      await http.get(Uri.parse('https://projetcube.tech/Api/RessourceAccueil'));

  if (response.statusCode == 200) {
    var topShowsJson = jsonDecode(response.body) as List;
    return topShowsJson.map((show) => Show.fromJson(show)).toList();
  } else {
    throw Exception('Failed to load shows');
  }
}

class Show {
  int userId;
  int id;
  String title;
  String body;
  String auctor;
  int typedoc;
  String chemindoc;

  Show({
    required this.userId,
    required this.id,
    required this.title,
    required this.body,
    required this.auctor,
    required this.typedoc,
    required this.chemindoc,
  });

  factory Show.fromJson(Map<String, dynamic> json) {
    return Show(
      userId: json["idPersonne"],
      id: json["id"],
      title: json["nom"],
      body: json["type"]["libelle"],
      auctor: json["personne"]["nom"] + " " + json["personne"]["prenom"],
      typedoc: json["type"]["id"],
      chemindoc: json["fullfiledowload"],
    );
  }
}

//List ressources historique + Favorie

Future<List<ShowPreference>> fetchShowsPreference(
    String email, String motdepasse, String url) async {
  final responsetemp = await http.post(
      Uri.parse('https://projetcube.tech/Api/LoginfromMobile'),
      body: {"email": email, "motdepasse": motdepasse});

  if (responsetemp.statusCode == 200) {
    var login = jsonDecode(responsetemp.body);
    var data = LoginData.fromJson(login);

    final response =
        await http.post(Uri.parse(url), body: {"id": '${data.id}'});

    if (response.statusCode == 200) {
      var topShowsJson = jsonDecode(response.body) as List;
      return topShowsJson.map((show) => ShowPreference.fromJson(show)).toList();
    } else {
      throw Exception('Failed to load shows');
    }
  } else {
    throw Exception('Failed to load shows');
  }
}

class ShowPreference {
  int userId;
  int id;
  String title;
  String body;
  String auctor;
  int typedoc;
  String chemindoc;

  ShowPreference({
    required this.userId,
    required this.id,
    required this.title,
    required this.body,
    required this.auctor,
    required this.typedoc,
    required this.chemindoc,
  });

  factory ShowPreference.fromJson(Map<String, dynamic> json) {
    return ShowPreference(
      userId: json["idPersonne"],
      id: json["id"],
      title: json["nom"],
      body: json["type"]["libelle"],
      auctor: json["personne"]["nom"] + " " + json["personne"]["prenom"],
      typedoc: json["type"]["id"],
      chemindoc: json["fullfiledowload"],
    );
  }
}
