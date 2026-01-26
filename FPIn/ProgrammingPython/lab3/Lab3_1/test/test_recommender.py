import unittest
from pathlib import Path
from tempfile import TemporaryDirectory

from lab3.Lab3_1.Lab3_1 import Movies, History, Recommender


class RecommenderTestCase(unittest.TestCase):
    def setUp(self):
        self.tmp = TemporaryDirectory()
        self.base = Path(self.tmp.name)

        self.movies_file = self.base / "movies.txt"
        self.history_file = self.base / "history.txt"

    def tearDown(self):
        self.tmp.cleanup()

    def write_movies(self, lines: list[str]):
        self.movies_file.write_text("\n".join(lines) + "\n", encoding="utf-8")

    def write_history(self, lines: list[str]):
        self.history_file.write_text("\n".join(lines) + "\n", encoding="utf-8")

    def test_movies_load_and_get_title(self):
        self.write_movies([
            "1,The Matrix",
            "2,Interstellar",
            "3,Inception",
        ])

        movies = Movies(str(self.movies_file))

        self.assertEqual(movies.get_title(1), "The Matrix")
        self.assertEqual(movies.get_title(2), "Interstellar")
        self.assertEqual(movies.get_title(999), None)

    def test_history_load(self):
        self.write_history([
            "1,2,3",
            "2,3",
            "",
            "5",
        ])

        history = History(str(self.history_file))

        self.assertEqual(history.all(), [
            [1, 2, 3],
            [2, 3],
            [5],
        ])

    def test_parse_user_input(self):
        self.write_movies(["1,A"])
        self.write_history(["1,2"])

        movies = Movies(str(self.movies_file))
        history = History(str(self.history_file))
        rec = Recommender(movies, history)

        self.assertEqual(rec.parse_user_input("1,2,3"), [1, 2, 3])
        self.assertEqual(rec.parse_user_input("  10 , 20 "), [10, 20])
        self.assertEqual(rec.parse_user_input(""), [])
        self.assertEqual(rec.parse_user_input("   "), [])

    def test_recommend_returns_none_if_empty_input(self):
        self.write_movies(["1,A", "2,B"])
        self.write_history(["1,2"])

        movies = Movies(str(self.movies_file))
        history = History(str(self.history_file))
        rec = Recommender(movies, history)

        self.assertIsNone(rec.recommend_title_from_input(""))
        self.assertIsNone(rec.recommend_title_from_input("   "))

    def test_recommend_returns_none_if_no_similar_histories(self):
        self.write_movies([
            "1,A",
            "2,B",
            "3,C",
        ])
        self.write_history([
            "2,3",
            "3",
        ])

        movies = Movies(str(self.movies_file))
        history = History(str(self.history_file))
        rec = Recommender(movies, history)

        self.assertIsNone(rec.recommend_title_from_input("1"))

    def test_recommend_returns_none_if_no_candidates(self):
        self.write_movies([
            "1,A",
            "2,B",
        ])
        self.write_history([
            "1,2",
            "1,2",
        ])

        movies = Movies(str(self.movies_file))
        history = History(str(self.history_file))
        rec = Recommender(movies, history)

        self.assertIsNone(rec.recommend_title_from_input("1,2"))

    def test_recommend_best_candidate_by_global_views(self):
        self.write_movies([
            "1,A",
            "2,B",
            "3,C",
            "4,D",
        ])

        self.write_history([
            "1,2,3",
            "1,3",
            "2,4",
            "3",
            "3",
        ])

        movies = Movies(str(self.movies_file))
        history = History(str(self.history_file))
        rec = Recommender(movies, history)

        result = rec.recommend_title_from_input("1,2")
        self.assertEqual(result, "C")

    def test_recommend_returns_title_or_none_if_movie_id_unknown(self):
        self.write_movies([
            "1,A",
            "2,B",
        ])
        self.write_history([
            "1,2,3",
            "1,3",
            "2,3",
        ])

        movies = Movies(str(self.movies_file))
        history = History(str(self.history_file))
        rec = Recommender(movies, history)

        self.assertIsNone(rec.recommend_title_from_input("1,2"))


if __name__ == "__main__":
    unittest.main()