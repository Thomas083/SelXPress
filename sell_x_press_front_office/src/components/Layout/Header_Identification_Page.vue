<template>
    <div class="header-container">
        <div class="header-logo">
            <img class="logo" src="../../assets/Header/Logo.png" />
        </div>
    </div>
</template>

<script>
export default {
    name: "Header_Not_Registered",
};
</script>

<style scoped>
.header-container {
    background-color: var(--main-blue);
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