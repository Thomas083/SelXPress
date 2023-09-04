<template>
    <div class="header-container">
        <img class="logo" src="../../assets/Header/logo_back_office.png" v-on:click="goToHome"/>
        <div class="header-content-right">
            <div class="header-btns">
                <button class="btn btn-primary header-btn-admin" v-on:click="goToAdminPanel">Admin Panel</button>
                <button class="btn btn-secondary header-btn-add" @click="goToAddProduct()">Add Product</button>
            </div>
            <div class="header-admin">
                <h3 class="header-name" v-on:click="goToUserProfil">{{ username }},</h3>
                <h3 class="header-name" v-on:click="goToUserProfil">Administrator</h3>
            </div>
            <img class="logo-log-out" src="../../assets/Header/log-out.png" v-on:click="logOut" />
        </div>
    </div>
</template>

<script>
import { GET } from '@/api/axios';
import { ENDPOINTS } from '@/api/endpoints';


export default {
    name: "HeaderAdmin",
    methods: {
        goToHome() {
            this.$router.push({ path: '/' })
        },
        goToAdminPanel() {
            this.$router.push({ path: '/admin' })
        },
        goToUserProfil() {
            this.$router.push({ path: '/user' })
        },
        goToAddProduct() {
            this.$router.push({ path: '/add-product' });
        },
        logOut() {
            localStorage.clear();
            window.location.reload();
        },
    },
    data() {
        return {
            username: ''
        }
    },
    mounted() {
        GET(ENDPOINTS.GET_ONE_USER, JSON.parse(localStorage.getItem('user')).token)
            .then((response) => {
                this.username = response.data.username;
            })
            .catch((error) => {
                console.dir(error)
            });
    },
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

.header-btns {
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: 0.5rem;
}

.header-btn-admin,
.header-btn-add {
    border-radius: 9px;
    font-weight: bold;
}

.header-btn-admin {
    padding: 1rem 1.5rem;
}

.header-btn-add {
    padding: 0.5rem;
}

.header-admin {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
}

.header-name {
    color: var(--main-white);
    cursor: pointer;
}

.header-name:hover {
    text-decoration: underline;
}
</style>