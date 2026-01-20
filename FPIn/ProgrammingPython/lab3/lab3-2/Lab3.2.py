import sys
from pathlib import Path

def build_ranges(boundaries):
    b = sorted(int(x) for x in boundaries)
    ranges = []
    ranges.append((0, b[0]))
    for i in range(1, len(b)):
        ranges.append((b[i-1] + 1, b[i]))
    ranges.append((b[-1] + 1, None))
    return ranges

def label(r):
    lo, hi = r
    return f"{lo}+" if hi is None else f"{lo}-{hi}"

def contains(r, age):
    lo, hi = r
    return age >= lo if hi is None else lo <= age <= hi

def parse_person(line):
    name, age_str = line.rsplit(",", 1)
    return name.strip(), int(age_str.strip())

def main():
    prog = Path(sys.argv[0]).name
    if len(sys.argv) < 2:
        print(f"Использование: python {prog} 18 25 35 45 60 80 100", file=sys.stderr)
        sys.exit(2)

    ranges = build_ranges(sys.argv[1:])
    groups = {r: [] for r in ranges}

    for raw in sys.stdin:
        s = raw.strip()
        if not s:
            continue
        if s == "END":
            break
        name, age = parse_person(s)
        for r in ranges:
            if contains(r, age):
                groups[r].append((name, age))
                break

    lines = []
    for r in reversed(ranges):
        people = groups[r]
        if not people:
            continue
        people.sort(key=lambda x: (-x[1], x[0]))
        payload = ", ".join(f"{n} ({a})" for n, a in people)
        lines.append(f"{label(r)}: {payload}")

    print("\n\n".join(lines))

if __name__ == "__main__":
    main()