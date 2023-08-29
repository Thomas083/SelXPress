<template>
    <div class="user-container">
        <table class="table table-striped table-hover table-bordered">
            <thead class="table-dark">
                <tr>
                    <th scope="col">Actions</th>
                    <th scope="col">Id</th>
                    <th scope="col">Name</th>
                    <th scope="col">Type</th>
                    <th scope="col">Attribute</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td scope="row" class="action-create-btn">
                        <button class="btn btn-add btn-admin" v-on:click="createAttribute">
                            Create
                            <img src="../../assets/Admin/add-category.png" alt="create" />
                        </button>
                    </td>
                    <td>-</td>
                    <td><input-component @input="createData('name', $event)" /></td>
                    <td><input-component @input="createData('type', $event)" /></td>
                    <td><img class="attribute-img" src="../../assets/Admin/attribut.png" /></td>
                </tr>
                <tr v-for="attribute in attributes">
                    <td scope="row" class="action-btns">
                        <button class="btn btn-primary btn-admin" v-on:click="sendUpdateData(attribute.id, attribute.name, attribute.type)">
                            Update
                            <img src="../../assets/Admin/bouton-modifier.png" alt="modify" />
                        </button>
                        <button class="btn btn-secondary btn-delete btn-admin" v-on:click="deleteAttribute(attribute.id)">
                            Delete
                            <img src="../../assets/Admin/bouton-supprimer.png" alt="delete" />
                        </button>
                    </td>
                    <td>{{ attribute.id }}</td>
                    <td><input-component :value='attribute.name' @input="updateData(attribute.id, 'name', $event)" />
                    </td>
                    <td><input-component :value="attribute.type" @input="updateData(attribute.id, 'type', $event)" /></td>
                    <td><img class="attribute-img" src="../../assets/Admin/attribut.png" v-on:click="setSelectedAttributeIndex(attribute.id)" /></td>
                </tr>
            </tbody>
        </table>
        <table v-if="selectedAttributeIndex !== null" class="table table-striped table-hover table-bordered">
            <thead>
                <tr class="table-grey">
                    <th scope="col" colspan="4">{{ attributes[selectedAttributeIndex].name.toLocaleUpperCase() }} Value</th>
                </tr>
                <tr class="table-dark">
                    <th scope="col">Actions</th>
                    <th scope="col">Id</th>
                    <th scope="col">Key</th>
                    <th scope="col">Value</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td scope="row" class="action-create-btn">
                        <button class="btn btn-add btn-admin" v-on:click="createAttributeData(selectedAttributeIndex)">
                            Create
                            <img src="../../assets/Admin/add-category.png" alt="create" />
                        </button>
                    </td>
                    <td>-</td>
                    <td><input-component @input="createDataAttribute('key', $event)" /></td>
                    <td><input-component @input="createDataAttribute('value', $event)" /></td>
                </tr>
                <tr v-for="data in attributes[selectedAttributeIndex].attributeData">
                    <td scope="row" class="action-btns">
                        <button class="btn btn-primary btn-admin" v-on:click="sendUpdateAttributeData(data.id, data)">
                            Update
                            <img src="../../assets/Admin/bouton-modifier.png" alt="modify" />
                        </button>
                        <button class="btn btn-secondary btn-delete btn-admin">
                            Delete
                            <img src="../../assets/Admin/bouton-supprimer.png" alt="delete" />
                        </button>
                    </td>
                    <td>{{ data.id }}</td>
                    <td><input-component :value='data.key' @input="updateAttributeData(selectedAttributeIndex, data.id, 'key', $event)" />
                    </td>
                    <td><input-component :value="data.value" @input="updateAttributeData(selectedAttributeIndex, data.id, 'value', $event)" /></td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
import { DELETE, GET, POST, PUT } from '@/api/axios';
import InputComponent from '../global/InputComponent.vue';
import { ENDPOINTS } from '@/api/endpoints';
import { createToast } from 'mosha-vue-toastify';

export default {
    name: "AttributesTab",
    components: {   
        InputComponent
    },
    data() {
        return {
            selectedAttributeIndex: null,
            formAttributeType: {
                name: '',
                type: '',
            },
            formAttributeData: {
                key: '',
                value: '',
                attributeId: null,
            },
            attributes: null
        }
    },
    methods: {
        createData(key, value) {
            this.formAttributeType = Object.assign(this.formAttributeType, { [key]: value });
        },
        createDataAttribute(key, value) {
            this.formAttributeData = Object.assign(this.formAttributeData, { [key]: value });
        },
        setSelectedAttributeIndex(index) {
            if (this.selectedAttributeIndex === index - 1) this.selectedAttributeIndex = null;
            else this.selectedAttributeIndex = index - 1;
        },
        updateData(index, key, value) {
            this.attributes[index - 1][key] = value;
        },
        updateAttributeData(index_attribute, index_data, key, value) {
            this.attributes[index_attribute].attributeData[index_data - 1][key] = value;
        },
        sendUpdateData(id, name, type) {
            PUT(ENDPOINTS.UPDATE_ATTRIBUTE + `/${id}`, {name: name, type: type}, JSON.parse(localStorage.getItem('user')).token)
            .then(() => {
                createToast({ title: 'Updated Succesfuly', description: `You updated successfuly the ${id} attribute` }, { type: 'success', position: 'bottom-right' });
            })
            .catch(() => {
                createToast(`An error occured... Please try again`, { type: 'danger', position: 'bottom-right' });
            });
        },
        sendUpdateAttributeData(id, data) { 
            PUT(ENDPOINTS.UPDATE_ATTRIBUTE_DATA + `/${id}`, data, JSON.parse(localStorage.getItem('user')).token)
            .then(() => {
                createToast({ title: 'Updated Succesfuly', description: `You updated successfuly the ${id} attribute data` }, { type: 'success', position: 'bottom-right' });
            })
            .catch(() => {
                createToast(`An error occured... Please try again`, { type: 'danger', position: 'bottom-right' });
            });
        },
        createAttribute() {
            POST(ENDPOINTS.CREATE_ATTRIBUTE, this.formAttributeType, JSON.parse(localStorage.getItem('user')).token)
            .then(() => {
                createToast({ title: 'Created Succesfuly', description: `You created successfuly the ${this.formAttributeType.name.toLocaleUpperCase()} attribute` }, { type: 'success', position: 'bottom-right' });
            })
            .catch(() => {
                createToast(`An error occured... Please try again`, { type: 'danger', position: 'bottom-right' });
            });
        },
        createAttributeData(id) {
            this.formAttributeData.attributeId = id + 1
            POST(ENDPOINTS.CREATE_ATTRIBUTE_DATA, this.formAttributeData, JSON.parse(localStorage.getItem('user')).token)
            .then((response) => {
                createToast({ title: 'Created Succesfuly', description: `You created successfuly the ${this.formAttributeData.key.toLocaleUpperCase()} attribute data` }, { type: 'success', position: 'bottom-right' });
            })
            .catch(() => {
                createToast(`An error occured... Please try again`, { type: 'danger', position: 'bottom-right' });
            });
        },
        deleteAttribute(id) {
            DELETE(ENDPOINTS.DELETE_ATTRIBUTE + `/${id}`, JSON.parse(localStorage.getItem('user')).token)
            .then(() => {
                createToast({ title: 'Deleted Succesfuly', description: `You deleted successfuly the ${id} attribute` }, { type: 'success', position: 'bottom-right' });
            })
            .catch(() => {
                createToast(`An error occured... Please try again`, { type: 'danger', position: 'bottom-right' });
            });
        }
    },
    mounted() {
        GET(ENDPOINTS.GET_ALL_ATTRIBUTE)
        .then((response) => {
                this.attributes = response.data
            })
            .catch((error) => {
                console.dir(error)
            });
    }
}
</script>

<style scoped>
.user-container {
    padding: 1rem;
}

img {
    height: 1rem;
}

.attribute-img {
    height: 1.5rem;
    cursor: pointer;
}

.btn-admin {
    border-radius: 1rem;
    box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
    display: flex;
    flex-direction: row;
    gap: 0.5rem;
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

.table-grey {
    height: 6rem;
    vertical-align: middle;
    font-size: 1.8rem;
    --bs-table-bg: var(--main-grey-table);
    --bs-table-color: var(--main-white)
}
</style>