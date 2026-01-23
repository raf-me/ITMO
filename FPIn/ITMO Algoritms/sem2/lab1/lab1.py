
class main:
    read = (open(f'Test1-1Outfile.txt'))
    test = read.read().split(' ')  # Открываем и читаем файл.

    list = [x for x in test]  # Заполняем список из test
    lab = []

    for i in list:  # Избавляемся от символа '\n', который разделяет строки в списке
        constant = i.split('\n')
        lab.extend(constant)

    def __init__(self, one):
        self.one = one

    @property
    def one(self, lab):
        return lab.summa()