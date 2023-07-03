<template>
  <Header />
  <!-- <Footer/> -->
</template>

<script>
// @ is an alias to /src
import Header from '@/components/Layout/Header.vue';
import Footer from '@/components/Layout/Footer.vue';

export default {
  name: 'HomeView',
  components: {
    Header,
    Footer
  }
}
</script>
