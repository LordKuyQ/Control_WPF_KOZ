INSERT INTO status (content) VALUES
('в наличии'),
('выдана'),
('на обслуживании');

INSERT INTO genre (name, description) VALUES
('Фантастика', 'Литература'),
('Детектив', 'Литература'),
('Романтика', 'Литература'),
('Ужасы', 'Литература'),
('Артхаус', 'Литература.');

INSERT INTO book (id, name, genre_id, description, date_release, status_id) VALUES
('1', 'Дом листьев', 5, 'Дом синий, который внутри на 1 сантиметр больше, чем снаружи...', '2016-04-25', 1);
