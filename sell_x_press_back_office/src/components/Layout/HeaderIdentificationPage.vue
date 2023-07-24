<template>
    <div class="header-container">
        <div class="header-logo">
            <img class="logo" src="../../assets/Header/logo_back_office.png" @click="goToHome()" />
        </div>
    </div>
</template>

<script>
export default {
    name: "HeaderIdentificationPage",
    methods: {
        goToHome() {
            this.$router.push({ path: '/' });
        },
    }
};
</script>

<style scoped>
.header-container {
    background-color: var(--main-red);
    display: flex;
    align-items: center;
    justify-content: center;
    width: 100vw;
    height: 20vh;
}

.header-logo {
    height: inherit;
}

.logo {
    height: inherit;
}
</style>