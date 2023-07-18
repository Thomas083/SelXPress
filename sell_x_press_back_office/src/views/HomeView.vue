<template>
  <div class="home-container">
    <categories-list />
    <div class="products-cards-container">
      <h1 class="products-category">Category: All</h1>
      <div class="products-cards">
        <product-card />
        <product-card />
        <product-card />
        <product-card />
        <product-card />
        <product-card />
        <product-card />
      </div>
    </div>
  </div>
</template>

<script>

import CategoriesList from "@/components/categories/CategoriesList.vue"
import ProductCard from "@/components/products/ProductsCard.vue"

export default {
  name: 'HomeView',
  components: {
    ProductCard,
    CategoriesList
  }
}
</script>

<style>
.home-container {
  padding: 2rem 1rem;
  display: flex;
  gap: 3rem;
}

.products-cards-container {
  display: flex;
  flex-direction: column;
  align-items: flex-start;
  gap: 1rem;
}

.products-category {
  font-weight: bold;
}

.products-cards {
  display: flex;
  flex-wrap: wrap;
  gap: 2rem;
}
</style>
