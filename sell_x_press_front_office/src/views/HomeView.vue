<template>
  <product-categories />
</template>

<script>
// @ is an alias to /src
import ProductCategories from '@/components/Home/ProductCategories.vue';

export default {
  name: 'HomeView',
  components: {
    ProductCategories,
  }
}
</script>
