<template>
    <div class="categories-tags-container">
        <div class="categories-table">
            <table class="table table-striped table-hover table-bordered" id="category-table">
                <thead class="table-dark">
                    <tr>
                        <th scope="col">Id</th>
                        <th scope="col">Name</th>
                        <th scope="col">Actions</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <th scope="row">-</th>
                        <td><input-component @input="createCategeoriesData('name', $event)" /></td>
                        <td class="action-create-btn">
                            <button class="btn btn-add btn-admin" v-on:click="createCategories">
                                Create
                                <img src="../../assets/Admin/add-category.png" alt="create" />
                            </button>
                        </td>
                    </tr>
                    <tr v-for="(category, index) in categories">
                        <th scope="row">{{ category.id }}</th>
                        <td><input-component :value='category.name'
                                @input="updateCategoriesData(index, 'name', $event)" /></td>
                        <td class="action-btns">
                            <button class="btn btn-primary btn-admin"
                                v-on:click="sendUpdateCategoriesData(category.id, category.name)">
                                Update
                                <img src="../../assets/Admin/bouton-modifier.png" alt="modify" />
                            </button>
                            <button class="btn btn-secondary btn-delete btn-admin"
                                v-on:click="deleteCategories(category.id)">
                                Delete
                                <img src="../../assets/Admin/bouton-supprimer.png" alt="delete" />
                            </button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="tags-table">
            <table class="table table-striped table-hover table-bordered">
                <thead class="table-dark">
                    <tr>
                        <th scope="col">Id</th>
                        <th scope="col">Name</th>
                        <th scope="col">CategoryId</th>
                        <th scope="col">Actions</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <th scope="row">-</th>
                        <td><input-component @input="createTagsData('name', $event)" /></td>
                        <td><input-component @input="createTagsData('categoryId', $event)" /></td>
                        <td class="action-create-btn">
                            <button class="btn btn-add btn-admin" v-on:click="createTags">
                                Create
                                <img src="../../assets/Admin/add-category.png" alt="create" />
                            </button>
                        </td>
                    </tr>
                    <tr v-for="(tag, index) in tags">
                        <th scope="row">{{ tag.id }}</th>
                        <td><input-component :value='tag.name' @input="updateTagsData(index, 'name', $event)" /></td>
                        <td><input-component :value='tag.categoryId'
                                @input="updateTagsData(tag.id, 'categoryId', $event)" />
                        </td>
                        <td class="action-btns">
                            <button class="btn btn-primary btn-admin" v-on:click="sendUpdateTagsData(tag.id, tag)">
                                Update
                                <img src="../../assets/Admin/bouton-modifier.png" alt="modify" />
                            </button>
                            <button class="btn btn-secondary btn-delete btn-admin" v-on:click="deleteTags(tag.id)">
                                Delete
                                <img src="../../assets/Admin/bouton-supprimer.png" alt="delete" />
                            </button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script>
import { GET, PUT, POST, DELETE } from "@/api/axios";
import { ENDPOINTS } from "@/api/endpoints";
import InputComponent from "@/components/global/InputComponent";
import { createToast } from "mosha-vue-toastify";

export default {
    name: "CategoriesTab",
    components: {
        InputComponent,
    },
    data() {
        return {
            formCategoriesData: {
                name: '',
            },
            formTagsData: {
                name: '',
                categoryId: null
            },
            categories: null,
            tags: null
        }
    },
    methods: {
        createCategeoriesData(key, value) {
            this.formCategoriesData = Object.assign(this.formCategoriesData, { [key]: value });
        },
        createTagsData(key, value) {
            this.formTagsData = Object.assign(this.formTagsData, { [key]: value });
        },
        updateCategoriesData(index, key, value) {
            this.categories[index][key] = value;
        },
        updateTagsData(index, key, value) {
            this.tags[index][key] = value;
        },
        sendUpdateCategoriesData(id, name) {
            PUT(ENDPOINTS.UPDATE_CATEGORY + `/${id}`, { name: name }, JSON.parse(localStorage.getItem('user')).token)
                .then(() => {
                    createToast({ title: 'Updated Succesfuly', description: `You updated successfuly the ${id} category` }, { type: 'success', position: 'bottom-right' });
                })
                .catch(() => {
                    createToast(`An error occured... Please try again`, { type: 'danger', position: 'bottom-right' });
                });
        },
        sendUpdateTagsData(id, tag) {
            PUT(ENDPOINTS.UPDATE_TAG + `/${id}`, { name: tag.name, categoryId: tag.categoryId }, JSON.parse(localStorage.getItem('user')).token)
                .then(() => {
                    createToast({ title: 'Updated Succesfuly', description: `You updated successfuly the ${id} tag` }, { type: 'success', position: 'bottom-right' });
                })
                .catch(() => {
                    createToast(`An error occured... Please try again`, { type: 'danger', position: 'bottom-right' });
                });
        },
        createCategories() {
            POST(ENDPOINTS.CREATE_CATEGORY, this.formCategoriesData, JSON.parse(localStorage.getItem('user')).token)
                .then(() => {
                    GET(ENDPOINTS.GET_ALL_CATEGORY)
                        .then((response) => {
                            this.categories = response.data
                            createToast({ title: 'Created Succesfuly', description: `You created successfuly the ${this.formCategoriesData.name.toLocaleUpperCase()} category` }, { type: 'success', position: 'bottom-right' });
                        })
                        .catch((error) => {
                            console.dir(error)
                        });
                })
                .catch(() => {
                    createToast(`An error occured... Please try again`, { type: 'danger', position: 'bottom-right' });
                });
        },
        deleteCategories(id) {
            DELETE(ENDPOINTS.DELETE_CATEGORY + `/${id}`, JSON.parse(localStorage.getItem('user')).token)
                .then(() => {
                    GET(ENDPOINTS.GET_ALL_CATEGORY)
                        .then((response) => {
                            this.categories = response.data
                            createToast({ title: 'Deleted Succesfuly', description: `You deleted successfuly the ${id} category` }, { type: 'success', position: 'bottom-right' });
                        })
                        .catch((error) => {
                            console.dir(error)
                        });
                })
                .catch(() => {
                    createToast(`An error occured... Please try again`, { type: 'danger', position: 'bottom-right' });
                });
        },
        createTags() {
            POST(ENDPOINTS.CREATE_TAG, this.formTagsData, JSON.parse(localStorage.getItem('user')).token)
                .then(() => {
                    GET(ENDPOINTS.GET_ALL_TAG)
                        .then((response) => {
                            this.tags = response.data
                            createToast({ title: 'Created Succesfuly', description: `You created successfuly the ${this.formTagsData.name.toLocaleUpperCase()} tag` }, { type: 'success', position: 'bottom-right' });
                        })
                        .catch((error) => {
                            console.dir(error)
                        });
                })
                .catch(() => {
                    createToast(`An error occured... Please try again`, { type: 'danger', position: 'bottom-right' });
                });
        },
        deleteTags(id) {
            DELETE(ENDPOINTS.DELETE_TAG + `/${id}`, JSON.parse(localStorage.getItem('user')).token)
                .then(() => {
                    GET(ENDPOINTS.GET_ALL_TAG)
                        .then((response) => {
                            this.tags = response.data
                            createToast({ title: 'Deleted Succesfuly', description: `You deleted successfuly the ${id} tag` }, { type: 'success', position: 'bottom-right' });
                        })
                        .catch((error) => {
                            console.dir(error)
                        });
                })
                .catch(() => {
                    createToast(`An error occured... Please try again`, { type: 'danger', position: 'bottom-right' });
                });
        },
    },
    mounted() {
        GET(ENDPOINTS.GET_ALL_CATEGORY)
            .then((response) => {
                this.categories = response.data
            })
            .catch((error) => {
                console.dir(error)
            });
        GET(ENDPOINTS.GET_ALL_TAG)
            .then((response) => {
                this.tags = response.data
            })
            .catch((error) => {
                console.dir(error)
            });
    }
}

</script>

<style scoped>
.categories-tags-container {
    display: flex;
    flex-direction: row;
    justify-content: center;
    gap: 5rem;
    padding: 1rem;
}

img {
    height: 1rem;
}

.action-create-btn {
    display: flex;
    justify-content: center;
}

.action-btns {
    display: flex;
    gap: 1rem;
    justify-content: center;
    align-items: center;
}

.btn-admin {
    border-radius: 1rem;
    box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
}

.btn-add {
    background-color: var(--main-orange);
    color: var(--main-white);
}

.btn-delete {
    background-color: var(--main-red);
    color: var(--main-white);
}

.table {
    --bs-table-border-color: var(--main-black);
}

.table-dark {
    --bs-table-bg: var(--main-green)
}
</style>