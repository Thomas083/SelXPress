<template>
  <HeaderIdentificationPage />
  <br>
  <HeaderNotAdmin />
  <br>
  <HeaderAdmin />
  <br>
  <FooterLayout />
</template>

<script>
// @ is an alias to /src
import HeaderIdentificationPage from '@/components/Layout/HeaderIdentificationPage.vue';
import HeaderNotAdmin from '@/components/Layout/HeaderNotAdmin.vue';
import HeaderAdmin from '@/components/Layout/HeaderAdmin.vue';
import FooterLayout from '@/components/Layout/FooterLayout.vue';

export default {
  name: 'HomeView',
  components: {
    HeaderIdentificationPage,
    HeaderNotAdmin,
    HeaderAdmin,
    FooterLayout
  }
}
</script>
