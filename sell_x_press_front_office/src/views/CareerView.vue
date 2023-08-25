<template>
    <div class="career-container">
        <h1>Career at SelXPress</h1>
        <p class="short-description">
            A Marketplace Platform To Connect Buyers And Sellers !
        </p>
        <h2>About SelXPress</h2>
        <p class="detailed-description">
            SelXPress is an innovative online marketplace platform that connects buyers and sellers.
            Our platform provides a seamless experience for both customers and sellers, offering a range
            of features to facilitate efficient transactions and manage the entire marketplace ecosystem.
        </p>
        <div class="separation-line"></div>
        <h2>Front Office and Back Office</h2>
        <p class="detailed-description">
            SelXPress is an online marketplace platform that offers both a front office interface,
            accessible to customers, a back office interface accessible to sellers and operators,
            as well as an API for managing data transit.
            <br>
            <br>The SelXPress front office provides customers with a user-friendly and secure access to
            purchase a variety of products. Users can browse a comprehensive list of items, filter results
            based on their preferences, view detailed product pages, add items to their cart, make secure payments, and
            review their order history.

            <br>The SelXPress back office is reserved for sellers and operators. It allows them to manage their products,
            handle inventory, and track comments and reviews. The back office also provides additional features for
            operators, such as user management and other administrative tasks related to marketplace operation.
            <br>
            <br>With SelXPress, customers can enjoy a convenient and secure online shopping experience, while sellers and
            operators benefit from dedicated access to manage their sales activities and ensure the smooth operation of the
            marketplace.
        </p>
    </div>
</template>
  
<script>
export default {
    name: 'Career',
};
</script>
  
<style scoped>
.career-container {
    max-width: 800px;
    margin: 0 auto;
    padding: 2rem;
    background-color: var(--main-white);
    border-radius: 1rem;
    text-align: justify;
}

.separation-line {
    border: 1px var(--main-grey-separation) solid;
    width: 100%;
}

h1 {
    font-size: 2.5rem;
    margin-bottom: 1rem;
}

h2 {
    font-size: 2rem;
    margin-top: 2rem;
}

h3 {
    font-size: 1.5rem;
    margin-top: 1.5rem;
}

.short-description {
    font-size: 1.2rem;
    margin-top: 1rem;
    color: var(--main-blue);
}

.detailed-description {
    font-size: 1rem;
    margin-top: 1rem;
    text-align: justify;
}</style>
  