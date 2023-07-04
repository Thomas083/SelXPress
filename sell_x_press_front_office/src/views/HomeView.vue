<template>
  <header-not-registered />
  <product-categories />
  <!-- <header-regitered />
  <header-identification-page /> -->
  <footer-layout />
</template>

<script>
// @ is an alias to /src
import HeaderNotRegistered from '@/components/Layout/HeaderNotRegistered.vue';
import HeaderRegitered from '@/components/Layout/HeaderRegitered.vue';
import HeaderIdentificationPage from '@/components/Layout/HeaderIdentificationPage.vue';
import FooterLayout from '@/components/Layout/FooterLayout.vue';
import ProductCategories from '@/components/Home/ProductCategories.vue';

export default {
  name: 'HomeView',
  components: {
    HeaderNotRegistered,
    HeaderRegitered,
    HeaderIdentificationPage,
    FooterLayout,
    ProductCategories,
  }
}
</script>
