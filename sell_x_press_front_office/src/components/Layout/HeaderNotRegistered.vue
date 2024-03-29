<template>
    <header class="header-container">
        <div class="header-logo">
            <img @click="goToHome()" class="logo" src="../../assets/Header/Logo.png" />
        </div>
        <div class="header-search">
            <select v-model="selectedOption" class="select-categories" @change="setCatagoryData">
                <option v-for="category in categoryList" :key="category.id" :value="category.name">{{ category.name }}</option>
            </select>
            <input v-model="formData.search" class="search-input" type="text" placeholder="Search in SelXpress...">
            <button class="loop" v-on:click="sendSearchData"><img src="../../assets/Header/loop.png" /></button>
        </div>
        <div class="header-login">
            <p class="welcome-login">Welcome,</p>
            <div class="btn-container">
                <button class="btn btn-primary btn-signin" @click="goToSignIn()">Sign In</button>
                <button class="btn btn-secondary btn-signup" @click="goToSignUp()">Sign Up</button>
            </div>
        </div>
        <div class="header-order">
            <button class="order-logo-btn" @click="goToCart()">
                <img class='order-logo' src="../../assets/Header/panier.png" />
            </button>
        </div>
    </header>
</template>
  
<script>
import { GET } from '@/api/axios';
import { ENDPOINTS } from '@/api/endpoints';
import { createToast } from 'mosha-vue-toastify';


export default {
    name: "HeaderNotRegistered",
    data() {
        return {
            selectedOption: 'Select a category',
            formData: {
                search: '',
                categoryId: 0
            },
            categories: null
        }
    },
    methods: {
        goToHome() {
            this.$router.push({ path: '/' });
        },
        goToSignIn() {
            this.$router.push({ path: '/login' });
        },
        goToSignUp() {
            this.$router.push({ path: '/register' });
        },
        goToCart() {
            createToast('You need to be connected to see your cart', { type: 'warning', position: 'bottom-right' });
        },
        setCatagoryData() {
            this.formData.categoryId = this.categoryList.find((category) => category.name === this.selectedOption).id;
        },
        sendSearchData() {
            if (this.formData.categoryId === 0) createToast(`Please select a category to search your product`, { type: 'info', position: 'bottom-right' });
            else if (this.formData.search === '' || this.formData.search === null) this.$router.push({ path: `/products/${this.formData.categoryId}/all`});
            else this.$router.push({ path: `/products/${this.formData.categoryId}/${this.formData.search}`});
        }
    },
    computed: {
        categoryList() {
            const newList = this.categories
            if (newList !== null) {
                newList.unshift({
                    id: 0,
                    name: 'Select a category',
                    tags: []
                });
                return newList
            }
        }
    },
    mounted() {
        GET(ENDPOINTS.GET_ALL_CATEGORIES)
            .then((response) => {
                this.categories = response.data
            })
            .catch((error) => {
                console.dir(error)
            });
    },
};
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.header-container {
    background-color: var(--main-blue);
    display: grid;
    grid-template-columns: 10vw 60vw 15vw 15vw;
    width: 100vw;
    height: auto;
}

.header-logo {
    grid-column: 1;
    height: 20vh;
}

.logo {
    height: inherit;
    cursor: pointer;
}

.header-search {
    grid-column: 2;
    border-radius: 9px;
    height: 20vh;
    display: flex;
    align-items: center;
}

.select-categories {
    height: 10vh;
    background-color: var(--main-grey-separation);
    border-radius: 9px 0 0 9px;
    border: none;
}

.search-input {
    height: 10vh;
    width: 80vw;
    border: none;
    padding-left: 1rem;
}

.loop {
    display: flex;
    align-items: center;
    padding: 1rem;
    justify-content: center;
    background-color: var(--main-orange);
    border-radius: 0 9px 9px 0;
    border: none;
    height: 10vh;
}

.header-login {
    grid-column: 3;
    color: var(--main-white);
    font-family: url(../../font/Inter/static/Inter-SemiBold.ttf);
    font-size: 1.5rem;
    height: 20vh;
}

.btn-container {
    display: flex;
    flex-direction: column;
    align-items: center;
    height: inherit;
}

.btn-signin,
.btn-signup {
    border-radius: 9px;
    display: flex;
    justify-content: center;
    align-items: center;
    font-size: 1rem;
    height: 1vh;
    margin-bottom: 0.5rem;

    border: none;
}

.btn-signin {
    padding: 1rem 2rem;

}

.btn-signup {
    padding: 1rem 3rem;
}

.header-order {
    grid-column: 4;
    height: 20vh;
    display: flex;
    flex-direction: row;
}

.order-logo-btn {
    background-color: transparent;
    border: none;
}

.order-logo {
    height: 15vh;
}

.circle-number {
    background-color: var(--main-orange);
    border-radius: 30px;
    padding: 0.5rem;
    margin-top: 1vh;
    color: var(--main-white);
    font-family: url(../../font/Inter/static/Inter-SemiBold.ttf);
    font-size: 2rem;
    height: 5vh;
    display: flex;
    align-items: center;
}

@media screen and (max-width: 1080px) {

    .header-container {
        background-color: var(--main-blue);
        display: grid;
        grid-template-columns: 33vw 33vw 33vw;
        grid-template-rows: auto auto;
        width: 100vw;
        align-items: center;
        height: auto;
    }

    .header-logo {
        grid-column: 1;
        grid-row: 1;
        height: 10vh;
    }

    .header-search {
        grid-row: 2;
        grid-column: 1/3;
    }

    .header-login {
        grid-row: 1;
        grid-column: 2;
    }

    .header-order {
        grid-row: 1;
        grid-column: 3;
    }

    .welcome-login {
        display: none;
    }

    .btn-signin {
        margin-top: 1rem;
    }

    .btn-signup {
        padding: 1rem 2rem;
    }

    .order-logo {
        height: 10vh;
    }

}
</style>
  