<template>
    <div class="header-container">
        <img @click="goToHome()" class="logo" src="../../assets/Header/logo_back_office.png" />
        <div class="header-content-right">
            <button class="header-btn-add" @click="goToAddProduct()">Add Product</button>
            <h3 class="header-name" @click="goToUserProfile()">Elsharion</h3>
            <img class="logo-log-out" src="../../assets/Header/log-out.png" />
        </div>
    </div>
</template>

<script>

export default {
    name: "HeaderNotAdmin",
    methods: {
    goToHome() {
        this.$router.push({ path: '/' });
    },
    goToUserProfile() {
        this.$router.push({ path: '/user' });
    },
    goToAddProduct() {
        this.$router.push({ path: '/add-product' });
    },
  }
};

</script>

<style scoped>

.header-container {
    display: flex;
    flex-direction: row;
    align-items: center;    
    width: 100vw;
    background-color: var(--main-red);
}

.header-content-right {
    display: flex;
    flex-direction: row;
    justify-content: flex-end;
    align-items: center;
    gap: 3rem;
    width: 100vw;
    margin-right: 3rem;
}

.logo {
    height: 20vh;
    cursor: pointer;
}

.logo-log-out {
    height: 10vh;
    cursor: pointer;
}

.logo-log-out:hover {
    opacity: 0.7;
}

.header-btn-add {
    border-radius: 9px;
    padding: 1rem;
    display: flex;
    align-items: center;
    justify-content: center;
    font-weight: bold;
    color: var(--main-white);
    border: none;
    box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
    background-color: var(--main-green);
}

.header-btn-add:hover {
    opacity: 0.7;
}

.header-name {
    color: var(--main-white);
    cursor: pointer;
}

.header-name:hover {
    text-decoration: underline;
}

</style>