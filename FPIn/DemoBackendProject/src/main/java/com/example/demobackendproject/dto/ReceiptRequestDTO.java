package com.example.demobackendproject.dto;

import java.util.List;

public class ReceiptRequestDTO {
    private String receiptId;
    private List<CategoryDTO> categories;

    public String getReceiptId() {
        return receiptId;
    }

    public void setReceiptId(String receiptId) {
        this.receiptId = receiptId;
    }

    public List<CategoryDTO> getCategories() {
        return categories;
    }

    public void setCategories(List<CategoryDTO> categories) {
        this.categories = categories;
    }
}