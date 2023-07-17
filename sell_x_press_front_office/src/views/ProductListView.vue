<template>
    <div class="product-list-container">
        <tags-list />
        <div class="products-cards">
            <product-card />
            <product-card />
            <product-card />
            <product-card />
            <product-card />
            <product-card />
            <product-card />
            <product-card />
        </div>
    </div>
</template>

<script>

import ProductCard from "@/components/products/ProductCard.vue";
import TagsList from "@/components/tags/TagsList.vue"

export default {
    name: 'ProductListView',
    components: {
        ProductCard,
        TagsList
    }
}

</script>

<style scoped>
.product-list-container {
    padding: 2rem 1rem;
    display: flex;
    gap: 3rem;
}

.products-cards {
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;
    gap: 2rem;
}
</style>