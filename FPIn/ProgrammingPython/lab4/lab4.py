import re
from collections import Counter


PHONE_RE = re.compile(r"^\+\d-\d{3}-\d{3}-\d{2}-\d{2}$")
PRIORITY_WEIGHT = {"MAX": 0, "MIDDLE": 1, "LOW": 2}


def normalize_products(products: str) -> str:
    items = [p.strip() for p in products.split(",") if p.strip()]
    counts = Counter(items)

    result = []
    for name in items:
        if name not in counts:
            continue
        c = counts.pop(name)
        if c > 1:
            result.append(f"{name} x{c}")
        else:
            result.append(name)

    return ", ".join(result)


def parse_address(address: str):
    parts = [p.strip() for p in address.split(".") if p.strip()]
    if len(parts) != 4:
        return None
    country, region, city, street = parts
    return country, region, city, street


def validate_phone(phone: str) -> bool:
    return bool(PHONE_RE.match(phone))


def main():
    valid_orders = []
    invalid_lines = []

    with open("orders.txt", "r", encoding="utf-8") as f:
        lines = [line.strip() for line in f if line.strip()]

    for line in lines:
        parts = line.split(";")
        if len(parts) != 6:
            continue

        order_id, products, fio, address, phone, priority = parts

        address_error = False
        phone_error = False

        if not address:
            address_error = True
            invalid_lines.append(f"{order_id};1;no data")
        else:
            addr = parse_address(address)
            if addr is None:
                address_error = True
                invalid_lines.append(f"{order_id};1;{address}")

        if not phone:
            phone_error = True
            invalid_lines.append(f"{order_id};2;no data")
        else:
            if not validate_phone(phone):
                phone_error = True
                invalid_lines.append(f"{order_id};2;{phone}")

        if not address_error and not phone_error:
            country, region, city, street = parse_address(address)
            valid_orders.append(
                {
                    "order_id": order_id,
                    "products": normalize_products(products),
                    "fio": fio,
                    "country": country,
                    "address_short": f"{region}. {city}. {street}",
                    "phone": phone,
                    "priority": priority,
                }
            )

    def sort_key(o):
        return (o["country"], PRIORITY_WEIGHT[o["priority"]])

    valid_orders.sort(key=sort_key)

    with open("order_country.txt", "w", encoding="utf-8") as f:
        for o in valid_orders:
            f.write(
                f"{o['order_id']};{o['products']};{o['fio']};{o['address_short']};{o['phone']};{o['priority']}\n"
            )

    with open("non_valid_orders.txt", "w", encoding="utf-8") as f:
        for e in invalid_lines:
            f.write(e + "\n")


if __name__ == "__main__":
    main()