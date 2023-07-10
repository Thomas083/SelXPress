<template>
  <HeaderIdentificationPage />
  <br>
  <HeaderNotAdmin />
</template>

<script>
// @ is an alias to /src
import HeaderIdentificationPage from '@/components/Layout/HeaderIdentificationPage.vue';
import HeaderNotAdmin from '@/components/Layout/HeaderNotAdmin.vue'

export default {
  name: 'HomeView',
  components: {
    HeaderIdentificationPage,
    HeaderNotAdmin
  }
}
</script>
