<template>
    <select v-model="selectedValue">
        <option v-for="attribute in attributes" :key="attribute.name" :value="attribute.name">
            {{ attribute.name }}
        </option>
    </select>
</template>

<script>
export default {
    name: 'SelectComponent',
    props: {
        attributes: Array
    },
    data() {
        return {
            selectedValue: ''
        }
    },
    watch: {
        selectedValue(newValue) {
            const attribute = {name: newValue, value: newValue}
            if (attribute) {
                this.$emit('selectedAttribute', attribute);
            }
        }
    }
}
</script>

<style scoped>
</style>
