def split_expression(expression: str) -> list[str]:
    parts: list[str] = []
    i = 0

    while i < len(expression):
        ch = expression[i]

        if ch.isspace():
            i += 1
            continue

        if ch in "+-*/()":
            parts.append(ch)
            i += 1
            continue

        if ch.isdigit() or ch == ".":
            start = i
            dot_count = 0

            while i < len(expression) and (expression[i].isdigit() or expression[i] == "."):
                if expression[i] == ".":
                    dot_count += 1
                    if dot_count > 1:
                        raise ValueError("Неверный формат числа")
                i += 1

            parts.append(expression[start:i])
            continue

        raise ValueError("Недопустимый символ")

    return parts


def is_number(part: str) -> bool:
    if part == ".":
        return False
    try:
        float(part)
        return True
    except ValueError:
        return False


def apply_unary_minus(parts: list[str]) -> list[str]:
    result: list[str] = []
    i = 0

    while i < len(parts):
        current = parts[i]

        if current == "-" and (i == 0 or parts[i - 1] in "+-*/("):
            if i + 1 >= len(parts):
                raise ValueError("Минус без числа")

            next_part = parts[i + 1]

            if is_number(next_part):
                result.append(str(-float(next_part)))
                i += 2
                continue

            if next_part == "(":
                result.append("0")
                result.append("-")
                i += 1
                continue

            raise ValueError("Ошибка унарного минуса")

        result.append(current)
        i += 1

    return result


def precedence(op: str) -> int:
    if op in ("*", "/"):
        return 2
    if op in ("+", "-"):
        return 1
    return 0


def to_rpn(parts: list[str]) -> list[str]:
    output: list[str] = []
    stack: list[str] = []

    for part in parts:
        if is_number(part):
            output.append(part)
            continue

        if part in "+-*/":
            while stack and stack[-1] in "+-*/" and precedence(stack[-1]) >= precedence(part):
                output.append(stack.pop())
            stack.append(part)
            continue

        if part == "(":
            stack.append(part)
            continue

        if part == ")":
            while stack and stack[-1] != "(":
                output.append(stack.pop())
            if not stack or stack[-1] != "(":
                raise ValueError("Скобки не совпадают")
            stack.pop()
            continue

        raise ValueError("Ошибка выражения")

    while stack:
        top = stack.pop()
        if top in ("(", ")"):
            raise ValueError("Скобки не совпадают")
        output.append(top)

    return output


def eval_rpn(rpn: list[str]) -> float:
    stack: list[float] = []

    for part in rpn:
        if is_number(part):
            stack.append(float(part))
            continue

        if part in "+-*/":
            if len(stack) < 2:
                raise ValueError("Недостаточно значений")

            b = stack.pop()
            a = stack.pop()

            if part == "+":
                stack.append(a + b)
            elif part == "-":
                stack.append(a - b)
            elif part == "*":
                stack.append(a * b)
            elif part == "/":
                if b == 0:
                    raise ZeroDivisionError("Деление на ноль")
                stack.append(a / b)

            continue

        raise ValueError("Ошибка вычисления")

    if len(stack) != 1:
        raise ValueError("Неверное выражение")

    return stack[0]


def calculate(expression: str) -> float:
    parts = split_expression(expression)
    parts = apply_unary_minus(parts)
    rpn = to_rpn(parts)
    return eval_rpn(rpn)


def main() -> None:
    while True:
        expr = input("Введите выражение (или exit): ").strip()

        if expr.lower() in ("exit", "quit"):
            break

        if not expr:
            print("Пустой ввод")
            continue

        try:
            result = calculate(expr)

            if result.is_integer():
                print(int(result))
            else:
                print(result)

        except Exception as e:
            print(f"Ошибка: {e}")


if __name__ == "__main__":
    main()
