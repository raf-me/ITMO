import pathlib
import random
import typing as tp

T = tp.TypeVar("T")


def read_sudoku(path: tp.Union[str, pathlib.Path]) -> tp.List[tp.List[str]]:
    path = pathlib.Path(path)
    with path.open() as f: puzzle = f.read()
    return create_grid(puzzle)


def create_grid(puzzle: str) -> tp.List[tp.List[str]]:
    digits = [c for c in puzzle if c in "123456789."]
    if len(digits) != 81: raise ValueError(f"Puzzle must contain 81 cells, got {len(digits)}")
    grid = group(digits, 9)
    return grid

def display(grid: tp.List[tp.List[str]]) -> None:
    width = 2
    line = "+".join(["-" * (width * 3)] * 3)
    for row in range(9):
        print(
            "".join(
                grid[row][col].center(width) + ("|" if str(col) in "25" else "") for col in range(9)
            )
        )
        if str(row) in "25": print(line)

    print()


def group(values: tp.List[T], n: int) -> tp.List[tp.List[T]]:
    result = []
    for i in range(0, len(values), n): result.append(values[i : i + n])

    return result


def get_row(grid: tp.List[tp.List[str]], pos: tp.Tuple[int, int]) -> tp.List[str]:
    row, _ = pos
    return grid[row]


def get_col(grid: tp.List[tp.List[str]], pos: tp.Tuple[int, int]) -> tp.List[str]:
    _, col = pos
    return [grid[r][col] for r in range(len(grid))]


def get_block(grid: tp.List[tp.List[str]], pos: tp.Tuple[int, int]) -> tp.List[str]:
    row, col = pos
    start_row = (row // 3) * 3
    start_col = (col // 3) * 3

    result = []
    for r in range(start_row, start_row + 3):
        for c in range(start_col, start_col + 3): result.append(grid[r][c])

    return result


def find_empty_positions(grid: tp.List[tp.List[str]]) -> tp.Optional[tp.Tuple[int, int]]:
    for r in range(len(grid)):
        for c in range(len(grid[r])):
            if grid[r][c] == ".": return (r, c)

    return None


def find_possible_values(grid: tp.List[tp.List[str]], pos: tp.Tuple[int, int]) -> tp.Set[str]:
    used = set(get_row(grid, pos)) | set(get_col(grid, pos)) | set(get_block(grid, pos))
    all_values = set("123456789")
    return all_values - used


def solve(grid: tp.List[tp.List[str]]) -> tp.Optional[tp.List[tp.List[str]]]:
    empty_pos = find_empty_positions(grid)
    if empty_pos is None: return grid

    r, c = empty_pos
    possible = find_possible_values(grid, empty_pos)
    if not possible: return None

    for value in possible:
        grid[r][c] = value
        result = solve(grid)
        if result is not None: return result
        grid[r][c] = "."

    return None


def check_solution(solution: tp.List[tp.List[str]]) -> bool:
    if solution is None: return False

    required = set("123456789")

    for row in solution:
        if set(row) != required: return False

    for col in range(9):
        column = [solution[row][col] for row in range(9)]
        if set(column) != required: return False

    for br in range(0, 9, 3):
        for bc in range(0, 9, 3):
            block = []
            for r in range(br, br + 3):
                for c in range(bc, bc + 3):
                    block.append(solution[r][c])
            if set(block) != required: return False

    return True


def generate_sudoku(N: int) -> tp.List[tp.List[str]]:
    if N < 0: N = 0
    if N > 81: N = 81

    grid = [["." for _ in range(9)] for _ in range(9)]
    solved = solve(grid)

    if solved is None: return [["." for _ in range(9)] for _ in range(9)]

    full = [row[:] for row in solved]

    positions = [(r, c) for r in range(9) for c in range(9)]
    random.shuffle(positions)

    to_remove = 81 - N
    for i in range(to_remove):
        r, c = positions[i]
        full[r][c] = "."

    return full


if __name__ == "__main__":
    for fname in ["puzzle1.txt", "puzzle2.txt", "puzzle3.txt"]:
        grid = read_sudoku(fname)
        display(grid)
        solution = solve(grid)
        if not solution: print(f"Puzzle {fname} can't be solved")
        else: display(solution)