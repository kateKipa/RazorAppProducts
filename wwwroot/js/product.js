function confirmDelete(productId) {
    if (confirm("Are you sure you want to delete this product?")) {
        window.location.href = "/Products/" + productId + "/Delete"
    }
}
