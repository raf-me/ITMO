package main.java;
import java.util.List;

public class VariantMapper {
    private final List<Integer> availableVariants;

    public VariantMapper(List<Integer> availableVariants) {
        this.availableVariants = availableVariants;
    }

    public int mapVariant(String inputVariant) {
        if (availableVariants == null || availableVariants.isEmpty()) {
            throw new IllegalStateException("Список доступных вариантов пуст.");
        }

        try {
            int requested = Integer.parseInt(inputVariant);
            if (availableVariants.contains(requested)) {
                return requested;
            }
        } catch (NumberFormatException ignored) {
            // если введено не число — игнорируем
        }

        // Если такого варианта нет, выбираем псевдослучайный доступный:
        int hash = Math.abs(inputVariant.hashCode());
        int index = hash % availableVariants.size();
        return availableVariants.get(index);
    }
}
