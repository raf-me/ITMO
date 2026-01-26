from collections import Counter

class Movies:
    def __init__(self, path="movies.txt"):
        self.path = path
        self.by_id = {}
        self._load()

    def _load(self):
        with open(self.path, encoding="utf-8") as f:
            for line in f:
                line = line.strip()
                if not line:
                    continue
                movie_id, movie_title = line.split(",", 1)
                self.by_id[int(movie_id)] = movie_title.strip()

    def get_title(self, movie_id):
        return self.by_id.get(movie_id)


class History:
    def __init__(self, path="history.txt"):
        self.path = path
        self.histories = []
        self._load()

    def _load(self):
        with open(self.path, encoding="utf-8") as f:
            for line in f:
                line = line.strip()
                if not line:
                    continue

                ids = [int(x) for x in line.split(",") if x.strip()]
                if ids:
                    self.histories.append(ids)

    def all(self):
        return self.histories


class Recommender:
    def __init__(self, movies: Movies, histories: History):
        self.movies = movies
        self.histories = histories

    def parse_user_input(self, raw: str):
        return [int(x) for x in raw.split(",") if x.strip()]

    def recommend_title_from_input(self, raw_input: str):
        user_movies = self.parse_user_input(raw_input)
        if not user_movies:
            return None

        user_set = set(user_movies)
        similar = []

        for hist in self.histories.all():
            other_set = set(hist)
            overlap = len(user_set & other_set) / len(user_set)
            if overlap >= 0.5:
                similar.append(hist)

        candidates = []
        for hist in similar:
            for m in hist:
                if m not in user_set:
                    candidates.append(m)

        if not candidates:
            return None

        all_views = Counter()
        for hist in self.histories.all():
            all_views.update(hist)

        best_movie_id = max(candidates, key=lambda m: all_views[m])
        return self.movies.get_title(best_movie_id)


def main():
    movies = Movies("movies.txt")
    histories = History("history.txt")
    recommender = Recommender(movies, histories)

    user_input = input("Введите id фильмов через запятую: ").strip()
    title = recommender.recommend_title_from_input(user_input)

    if title:
        print("Согласно рекомендациям: " + title)
    else:
        print("Рекомендаций нет.")


if __name__ == "__main__":
    main()