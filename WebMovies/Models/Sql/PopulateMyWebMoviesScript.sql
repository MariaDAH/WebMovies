USE MyWebMovies

/* Tables depopulation ****************************************************** */

--DELETE FROM Favorite;
--DELETE FROM Comment;
--DELETE FROM Rating;
--DELETE FROM LinkLabel;
--DELETE FROM Link;
--DELETE FROM Label;
--DELETE FROM UserProfile;

/* Tables population ******************************************************** */

/* UserProfile */
INSERT INTO UserProfile (usrlogin, enPassword, firstName, lastName, email, [language], country) VALUES ('paco', 'iaDTDqG0y5r/hHWKQY6DXwaZP58kRoUe8zHTDCcXOCg=', 'Paco', 'Paco Paco', 'paco@paco.com', 'es', 'ES');
INSERT INTO UserProfile (usrlogin, enPassword, firstName, lastName, email, [language], country) VALUES ('yago', '6ZuNfb5vKleO/V9oIGss8QMK8W89Yoy1yAv6/OZ/HLg=', 'Yago', 'Méndez Vidal', 'yago.mendez.vidal@udc.es', 'gl', 'ES');
INSERT INTO UserProfile (usrlogin, enPassword, firstName, lastName, email, [language], country) VALUES ('maria', 'lK7J++2Yns4Ymn4XLJz0FmkFBJUVK8TB2/KjjX/YVic=', 'María', 'de Andrés Herrero', 'maria.deandres@udc.es', 'fr', 'ES');
INSERT INTO UserProfile (usrlogin, enPassword, firstName, lastName, email, [language], country) VALUES ('pepe', 'fJ58FJSyaEq3wZ1q/3N+Rg+p6Y1aI02hMQyX3fVpGDQ=', 'Pepe', 'Pepe Pepe', 'pepe@pepe.com', 'en', 'US');

/* Label */
INSERT INTO Label (labelText) VALUES ('imdb');
INSERT INTO Label (labelText) VALUES ('database');
INSERT INTO Label (labelText) VALUES ('english');
INSERT INTO Label (labelText) VALUES ('filmaffinity');
INSERT INTO Label (labelText) VALUES ('español');
INSERT INTO Label (labelText) VALUES ('google');
INSERT INTO Label (labelText) VALUES ('images');
INSERT INTO Label (labelText) VALUES ('official');

/* Link */
INSERT INTO Link (usrId, movieId, name, url, [description], [date], reportRead) VALUES (1, 1, 'IMDb', 'http://www.imdb.com/title/tt1655442/', 'Página de IMDb para "The Artist"', '10/09/2012 9:59:25', 0);
INSERT INTO Link (usrId, movieId, name, url, [description], [date], reportRead) VALUES (1, 1, 'FilmAffinity', 'http://www.filmaffinity.com/es/film207902.html', 'Ficha en FilmAffinity de "The Artist"', '10/09/2012 10:04:15', 0);
INSERT INTO Link (usrId, movieId, name, url, [description], [date], reportRead) VALUES (2, 1, 'Imaxes en Google', 'https://www.google.com/search?q=%22the+artist%22&tbm=isch', 'Procura de imaxes de "The Artist" en Google', '10/09/2012 10:09:54', 0);
INSERT INTO Link (usrId, movieId, name, url, [description], [date], reportRead) VALUES (3, 1, 'Site officiel', 'http://weinsteinco.com/sites/the-artist/', 'Site officiel pour "The Artist"', '10/09/2012 10:13:43', 0);

/* LinkLabel */
INSERT INTO LinkLabel (linkId, labelId) VALUES (1, 1);
INSERT INTO LinkLabel (linkId, labelId) VALUES (1, 2);
INSERT INTO LinkLabel (linkId, labelId) VALUES (2, 2);
INSERT INTO LinkLabel (linkId, labelId) VALUES (1, 3);
INSERT INTO LinkLabel (linkId, labelId) VALUES (4, 3);
INSERT INTO LinkLabel (linkId, labelId) VALUES (2, 4);
INSERT INTO LinkLabel (linkId, labelId) VALUES (2, 5);
INSERT INTO LinkLabel (linkId, labelId) VALUES (3, 6);
INSERT INTO LinkLabel (linkId, labelId) VALUES (3, 7);
INSERT INTO LinkLabel (linkId, labelId) VALUES (4, 8);

/* Rating */
INSERT INTO Rating (usrId, linkId, value, [date]) VALUES (3, 2, -1, '10/09/2012 10:14:26');
INSERT INTO Rating (usrId, linkId, value, [date]) VALUES (3, 1, 1, '10/09/2012 10:14:46');
INSERT INTO Rating (usrId, linkId, value, [date]) VALUES (2, 2, -1, '10/09/2012 10:15:18');
INSERT INTO Rating (usrId, linkId, value, [date]) VALUES (2, 1, 1, '10/09/2012 10:15:38');
INSERT INTO Rating (usrId, linkId, value, [date]) VALUES (4, 1, 1, '10/09/2012 11:03:26');

/* Comment */
DELETE FROM Comment;
INSERT INTO Comment (usrId, linkId, commentText, [date]) VALUES (2, 1, 'Moi útil', '10/09/2012 11:05:01');
INSERT INTO Comment (usrId, linkId, commentText, [date]) VALUES (3, 1, 'On est fièrt à notre cammarade et sa magnifique interpretacion à ce film!', '10/09/2012 11:11:22');
INSERT INTO Comment (usrId, linkId, commentText, [date]) VALUES (1, 1, '¡Gracias!', '10/09/2012 15:43:07');

/* Favorite */
INSERT INTO Favorite (usrId, linkId, name, [description], [date]) VALUES (1, 4, '"The Artist" oficial', 'Página oficial de "The Artist"', '10/09/2012 11:13:22');
INSERT INTO Favorite (usrId, linkId, name, [description], [date]) VALUES (1, 3, '"The Artist" imágenes', 'Imágenes en Google de "The Artist"', '10/09/2012 11:14:21');
INSERT INTO Favorite (usrId, linkId, name, [description], [date]) VALUES (2, 2, 'Ligazón denunciada', 'Exemplo de ligazón denunciada en favoritos', '10/09/2012 11:15:20');
INSERT INTO Favorite (usrId, linkId, name, [description], [date]) VALUES (2, 1, 'Ligazón promocionada', 'Exemplo de ligazón promocionada en favoritos', '10/09/2012 11:15:44');
INSERT INTO Favorite (usrId, linkId, name, [description], [date]) VALUES (2, 4, 'Ligazón normal', 'Exemplo de ligazón normal en favoritos', '10/09/2012 11:16:34');
