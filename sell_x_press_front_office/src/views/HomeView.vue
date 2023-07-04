<template>
  <!-- <Header /> -->
  <Header_Identification_Page />
  <!-- <Footer/> -->
  <!-- <Help /> -->
</template>

<script>
// @ is an alias to /src
import Header_Not_Registered from '@/components/Layout/Header_Not_Registered.vue';
import Header_Identification_Page from '@/components/Layout/Header_Identification_Page.vue';
import Footer from '@/components/Layout/Footer.vue';
import Help from '@/components/Layout/Help.vue'

export default {
  name: 'HomeView',
  components: {
    Header_Not_Registered,
    Header_Identification_Page,
    Footer,
    Help
  }
}
</script>
