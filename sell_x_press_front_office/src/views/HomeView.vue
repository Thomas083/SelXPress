<template>
  <header-not-registered />
  <header-regitered />
  <header-identification-page />
  <footer-layout />
  <!-- <help-modal /> -->
</template>

<script>
// @ is an alias to /src
import HeaderNotRegistered from '@/components/Layout/HeaderNotRegistered.vue';
import HeaderRegitered from '@/components/Layout/HeaderRegitered.vue';
import HeaderIdentificationPage from '@/components/Layout/HeaderIdentificationPage.vue';
import FooterLayout from '@/components/Layout/FooterLayout.vue';
import HelpModal from '@/components/Layout/HelpModal.vue';

export default {
  name: 'HomeView',
  components: {
    HeaderNotRegistered,
    HeaderRegitered,
    HeaderIdentificationPage,
    FooterLayout,
    HelpModal,
  }
}
</script>
